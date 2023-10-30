using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;

namespace VolgaIt.MediatR.AdminRentCommands.Update
{
    public class UpdateAdminRentRequestHandler : IRequestHandler<UpdateAdminRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateAdminRentRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(UpdateAdminRentRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsync(request.Id);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            if (Rent.RentTypes.Values.Contains(request.PriceType.ToUpper()) is false)
                return new BadRequestObjectResult(new { error = ActionMessages.UnknownRentType() });

            if (request.PriceOfUnit < 0)
                return new BadRequestObjectResult(new { error = ActionMessages.ParametrsMustGreaterZero(nameof(request.PriceOfUnit)) });

            if (await _userManager.ExistById(request.UserId) is false)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            var transport = _dataManager.Transports.GetById(request.TransportId);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            _mapper.Map(request, rent);

            _dataManager.Rents.Update(rent);
            await _dataManager.Rents.SaveChangesAsync();

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(new { message = "Updated", rentViewModel });

        }
    }
}
