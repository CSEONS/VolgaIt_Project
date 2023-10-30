using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;

namespace VolgaIt.MediatR.TransportCommands.Create
{
    public class CreateTransportRequestHandler : IRequestHandler<CreateTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CreateTransportRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(CreateTransportRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(request.User);

            Transport transport = _mapper.Map<CreateTransportRequest, Transport>(request);

            var transportTypes = await _dataManager.TransportTypes.GetAll();

            var transportType = transportTypes.FirstOrDefault(t => t.NormalizedName == request.TrnasportType.ToUpper());

            if (transportType is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UnknownTransportType() });

            transport.Type = transportType;
            transport.OwnerId = user?.Id;

            await _dataManager.Transports.AddAsync(transport);
            await _dataManager.Transports.SaveChangesAsyn();

            return new OkObjectResult(new { message = ActionMessages.TransportCreated(), id = transport.Id });
        }
    }
}
