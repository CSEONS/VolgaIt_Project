using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.TransportCommands.Update
{
    public class UpdateTransportRequestHandler : IRequestHandler<UpdateTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public UpdateTransportRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Handle(UpdateTransportRequest request, CancellationToken cancellationToken)
        {
            var transport = _mapper.Map<UpdateTransportRequest, Transport>(request);

            transport.Id = request.UpdatedTransportId;

            _dataManager.Transports.Update(transport);

            await _dataManager.Transports.SaveChangesAsyn();

            var transportViewModel = _mapper.Map<Transport, TransportViewModel>(transport);

            return new OkObjectResult(new { message = ActionMessages.TransportUpdated() });
        }
    }
}
