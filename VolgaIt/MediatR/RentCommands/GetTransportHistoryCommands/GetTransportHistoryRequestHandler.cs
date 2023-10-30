using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.Domain;
using VolgaIt.Service;
using AutoMapper.QueryableExtensions;
using VolgaIt.MediatR.RentCommands.Models;

namespace VolgaIt.MediatR.RentCommands.GetTransportHistoryCommands
{
    public class GetTransportHistoryRequestHandler : IRequestHandler<GetTransportHistoryRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetTransportHistoryRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetTransportHistoryRequest request, CancellationToken cancellationToken)
        {
            var transport = _dataManager.Transports.GetById(request.TransportId);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            var user = await _userManager.GetUserAsync(request.User);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            if (user.Id != transport.OwnerId)
                return new BadRequestObjectResult(new { error = ActionMessages.YouNotOwner() });

            var transportRentHistory = _dataManager.Rents.GetAll().Where(r => r.TransportId ==  request.TransportId);

            var rentViewModel = transportRentHistory.AsQueryable().ProjectTo<RentViewModel>(_mapper.ConfigurationProvider);

            return new OkObjectResult(rentViewModel);
        }
    }
}
