using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.GetTransportHistory
{
    public class GetAdminTransportHistoryRequestHandler : IRequestHandler<GetAdminTransportHistoryRequest, IActionResult>
    {

        private readonly DataManager _dataManagaer;
        private readonly IMapper _mapper;

        public GetAdminTransportHistoryRequestHandler(DataManager dataManager, IMapper mapper)
        {
            _dataManagaer = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAdminTransportHistoryRequest request, CancellationToken cancellationToken)
        {
            var transport = _dataManagaer.Transports.GetById(request.TransportId);

            if (transport is null)
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            var rents = _dataManagaer.Rents.GetAll().Where(r => r.TransportId == transport.Id).AsQueryable();

            var rentsViewModel = rents.ProjectTo<RentViewModel>(_mapper.ConfigurationProvider);

            return new OkObjectResult(rentsViewModel);
        }
    }
}
