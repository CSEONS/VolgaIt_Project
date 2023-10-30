using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.RentCommands.GetLoactionTransportsCommands
{
    public class GetLoactionTransportsRequestHandler : IRequestHandler<GetLoactionTransportsRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public GetLoactionTransportsRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetLoactionTransportsRequest request, CancellationToken cancellationToken)
        {
            var transportType =  await _dataManager.TransportTypes.GetByNameAsync(request.Type);

            if (transportType is null && request.Type.ToUpper() != TransportType.AllTypeNormalizedName)
                return new BadRequestObjectResult(new { error = ActionMessages.UnknownTransportType() });


            var location = new Location(request.Lat, request.Long);

            var transports = _dataManager.Transports
                .GetAll()
                .Where(t => location.IsWithinRadius
                    (
                        new Location(t.Latitude, t.Longitude),
                        request.Radius
                    ) && 
                    (
                        t.TypeId == transportType?.Id || request.Type.ToLower() == TransportType.AllTypeNormalizedName.ToLower()
                    ) &&
                    (
                        t.CanBeRented is true
                    ))
                .AsQueryable();

            var transportsViewModel = transports.ProjectTo<TransportViewModel>(_mapper.ConfigurationProvider);

            return new OkObjectResult(transportsViewModel);
        }
    }
}
