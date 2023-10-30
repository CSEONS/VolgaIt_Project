using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.Begin
{
    public class BeginAdminRentRequestHandler : IRequestHandler<BeginAdminRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public BeginAdminRentRequestHandler(DataManager dataManager,UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(BeginAdminRentRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });
            
            var transport = _dataManager.Transports.GetById(request.TransportId);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            if (request.PriceOfUnit < 0)
                return new BadRequestObjectResult(new { error = ActionMessages.ParametrsMustGreaterZero(nameof(request.PriceOfUnit)) });

            if (Rent.RentTypes.Values.Contains(request.PriceType.ToUpper()) is false)
                return new BadRequestObjectResult(new { error = ActionMessages.UnknownRentType() });

            Rent rent = new Rent()
            {
                Id = Guid.NewGuid().ToString(),
                StartTime = request.TimeStart,
                EndTime = request.TimeEnd,
                PriceOfUnit = request.PriceOfUnit,
                TransportId = request.TransportId,
                UserId = request.UserId,
                FinalPrice = request.FinalPrice,
                RentType = request.PriceType,
            };

            await _dataManager.Rents.AddAsync(rent);
            await _dataManager.Rents.SaveChangesAsync();

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(rentViewModel);
        }
    }
}
