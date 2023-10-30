using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.TransportCommands.Get
{
    public class GetTransportRequestHandler : IRequestHandler<GetTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public GetTransportRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetTransportRequest request, CancellationToken cancellationToken)
        {
            var transport = _dataManager.Transports.GetByIdEager(request.Id);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            var transportViewModel = _mapper.Map<Transport, TransportViewModel>(transport);

            transportViewModel.OwnerId = transport.OwnerId;

            return new OkObjectResult(transportViewModel);
        }
    }
}
