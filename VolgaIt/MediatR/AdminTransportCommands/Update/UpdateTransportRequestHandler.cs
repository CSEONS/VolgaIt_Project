using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.MediatR.TransportCommands.Update;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminTransportCommands.Update
{
    public class UpdateTransportRequestHandler : IRequestHandler<UpdateAdminTransportRequest, IActionResult>
    {
        private readonly IMapper _mapper;
        private readonly DataManager _dataManager;

        public UpdateTransportRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(UpdateAdminTransportRequest request, CancellationToken cancellationToken)
        {
            var transport = _dataManager.Transports.GetById(request.UpdatedTransportId);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            transport.CanBeRented = request.CanBeRented;
            transport.Identifier = request.Identifier;
            transport.DayPrice = request.DayPrice;
            transport.MinutePrice = request.MinutePrice;
            transport.Color = request.Color;
            transport.Description = request.Description;
            transport.Latitude = request.Latitude;
            transport.Longitude = request.Longitude;
            transport.Model = request.Model;

            //transport = _mapper.Map<UpdateAdminTransportRequest, Transport>(request);

            //ransport.Id = request.UpdatedTransportId;

            _dataManager.Transports.Update(transport);

            await _dataManager.Transports.SaveChangesAsyn();

            var transportViewModel = _mapper.Map<Transport, TransportViewModel>(transport);

            return new OkObjectResult(new { message = ActionMessages.TransportUpdated() });
        }
    }
}
