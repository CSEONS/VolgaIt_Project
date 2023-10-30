using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.RentCommands.BeginCommands
{
    public class BeginRentRequestHandler : IRequestHandler<BeginRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public BeginRentRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(BeginRentRequest request, CancellationToken cancellationToken)
        {
            if (Rent.RentTypes.Values.Contains(request.Type.ToUpper()) is false)
                return new BadRequestObjectResult(new { error = ActionMessages.UnknownRentType() });

            var transport = _dataManager.Transports.GetById(request.TransportId);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            if (transport.CanBeRented)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportRented() });

            var user = await _userManager.GetUserAsync(request.User);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            Rent rent = new Rent()
            {
                Id = Guid.NewGuid().ToString(),
                Transport = transport,
                User = user,
                StartTime = DateTime.UtcNow,
                RentType = request.Type,
                PriceOfUnit = request.Type == "DAYS" ? (double)transport.DayPrice : (double)transport.MinutePrice
            };

            _dataManager.Transports.Update(transport);
            await _dataManager.Transports.SaveChangesAsyn();

            await _dataManager.Rents.AddAsync(rent);
            await _dataManager.Rents.SaveChangesAsync();

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(rentViewModel);
        }
    }
}
