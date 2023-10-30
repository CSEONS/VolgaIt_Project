using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.Domain;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using VolgaIt.MediatR.TransportCommands.Models;
using AutoMapper.QueryableExtensions;

namespace VolgaIt.MediatR.AdminTransportCommands.GetQuerable
{
    public class GetQuarableAdminTransportRequestHandler : IRequestHandler<GetQuarableAdminTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public GetQuarableAdminTransportRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetQuarableAdminTransportRequest request, CancellationToken cancellationToken)
        {
            TransportType transportType = await _dataManager.TransportTypes.GetByNameAsync(request.Type);

            IEnumerable<Transport> transports = _dataManager.Transports
                    .GetAll()
                    .Where(t => request.Type.ToUpper() == TransportType.AllTypeNormalizedName || t.TypeId == transportType.Id)
                    .Skip(request.Start)
                    .Take(request.Count);

            var transportViewModel = transports.AsQueryable().ProjectTo<TransportViewModel>(_mapper.ConfigurationProvider);

            return new OkObjectResult(transportViewModel);
        }
    }
}
