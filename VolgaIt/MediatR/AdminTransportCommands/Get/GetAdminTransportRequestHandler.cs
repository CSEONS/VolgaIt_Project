using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Get;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminTransportCommands.Get
{
    public class GetAdminTransportRequestHandler : IRequestHandler<GetAdminTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public GetAdminTransportRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAdminTransportRequest request, CancellationToken cancellationToken)
        {
            var transport = _dataManager.Transports.GetById(request.Id);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            var transportViewModel = _mapper.Map<Transport, TransportViewModel>(transport);

            return new OkObjectResult(transportViewModel);
        }
    }
}
