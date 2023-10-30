using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.RentCommands.FinishCommands
{
    public class FinishRentRequestHandler : IRequestHandler<FinishRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public FinishRentRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Handle(FinishRentRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsyncEager(request.RentId);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            var transport = _dataManager.Transports.GetById(rent.TransportId);

            transport.Latitude = (double)request.Lat;
            transport.Longitude = (double)request.Longitude;
            transport.CanBeRented = true;

            _dataManager.Transports.Update(transport);
            await _dataManager.Transports.SaveChangesAsyn();

            rent.EndTime = DateTime.UtcNow;
            rent.FinalPrice = rent.CalculateFinalPrice();

            _dataManager.Rents.Update(rent);
            await _dataManager.Rents.SaveChangesAsync();

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(rentViewModel);
        }
    }
}
