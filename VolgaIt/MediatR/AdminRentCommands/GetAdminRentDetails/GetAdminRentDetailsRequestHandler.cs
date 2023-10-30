using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.GetAdminRentDetails
{
    public class GetAdminRentDetailsRequestHandler : IRequestHandler<GetAdminRentDetailsRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public GetAdminRentDetailsRequestHandler(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Handle(GetAdminRentDetailsRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsync(request.RentId);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(rentViewModel);
        }
    }
}
