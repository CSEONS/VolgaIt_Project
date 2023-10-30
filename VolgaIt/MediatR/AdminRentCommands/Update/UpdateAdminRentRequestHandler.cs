using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.Update
{
    public class UpdateAdminRentRequestHandler : IRequestHandler<UpdateAdminRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public UpdateAdminRentRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(UpdateAdminRentRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsync(request.RentId);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            if (Rent.RentTypes.Values.Contains(request.PriceType.ToUpper()) is false)
                return new BadRequestObjectResult(new { error = ActionMessages.UnknownRentType() });

            if (request.PriceOfUnit < 0)
                return new BadRequestObjectResult(new { error = ActionMessages.ParametrsMustGreaterZero(nameof(request.PriceOfUnit)) });

            rent = _mapper.Map<UpdateAdminRentRequest, Rent>(request);
            rent.Id = request.RentId;

            _dataManager.Rents.Update(rent);
            await _dataManager.Rents.SaveChangesAsync();

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(rentViewModel);
        }
    }
}
