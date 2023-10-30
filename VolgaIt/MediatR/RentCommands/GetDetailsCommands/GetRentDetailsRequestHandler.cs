using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.RentCommands.GetDetailsCommands
{
    public class GetRentDetailsRequestHandler : IRequestHandler<GetRentDetailsRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetRentDetailsRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetRentDetailsRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsync(request?.Id);
            var user = await _userManager.GetUserAsync(request?.User);
            var transport = _dataManager.Transports.GetById(rent?.TransportId);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            if(transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            if (user.Id != rent.UserId && user.Id != transport.OwnerId)
                return new BadRequestObjectResult(new { error = ActionMessages.AccesDined() });

            var rentViewModel = _mapper.Map<Rent, RentViewModel>(rent);

            return new OkObjectResult(rentViewModel);
        }
    }
}
