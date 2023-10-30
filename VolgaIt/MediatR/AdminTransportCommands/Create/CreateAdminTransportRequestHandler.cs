using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Create;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;

namespace VolgaIt.MediatR.AdminTransportCommands.Create
{
    public class CreateAdminTransportRequestHandler : IRequestHandler<CreateAdminTransportRequest, IActionResult>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly DataManager _dataManager;

        public CreateAdminTransportRequestHandler(IMapper mapper, UserManager<AppUser> userManager, DataManager dataManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Handle(CreateAdminTransportRequest request, CancellationToken cancellationToken)
        {
            var cretaedTransport = _mapper.Map<CreateAdminTransportRequest, Transport>(request);
            var user = await _userManager.GetUserAsync(request.User);
            var transportType = await _dataManager.TransportTypes.GetByNameAsync(request.TrnasportType);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            cretaedTransport.Owner = user;
            cretaedTransport.Type = transportType;

            await _dataManager.Transports.AddAsync(cretaedTransport);

            TransportViewModel transportView = _mapper.Map<Transport, TransportViewModel>(cretaedTransport);

            return new OkObjectResult(transportView);
        }
    }
}
