using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.MediatR.RentCommands.BeginCommands;
using VolgaIt.MediatR.RentCommands.FinishCommands;
using VolgaIt.MediatR.RentCommands.GetDetailsCommands;
using VolgaIt.MediatR.RentCommands.GetLoactionTransportsCommands;
using VolgaIt.MediatR.RentCommands.GetTransportHistoryCommands;
using VolgaIt.MediatR.RentCommands.MyHistoryCommands;

namespace VolgaIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Transport")]
        public async Task<IActionResult> GetLoactionTransports([FromQuery] GetLoactionTransportsRequest request,CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpGet("{rentId}")]
        [Authorize]
        public async Task<IActionResult> GetRentDetails(string rentId, CancellationToken cancellationToken)
        {
            GetRentDetailsRequest request = new GetRentDetailsRequest()
            {
                Id = rentId,
                User = User
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpGet("MyHistory")]
        [Authorize]
        public async Task<IActionResult> Create(MyRentHistoryRequest request, CancellationToken cancellationToken)
        {
            request.User = User;

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("New/{transportId}")]
        [Authorize]
        public async Task<IActionResult> Begin(string transportId, BeginRentRequest request, CancellationToken cancellationToken)
        {
            request.TransportId = transportId;
            request.User = User;

            return await _mediator.Send(request , cancellationToken);
        }

        [HttpPost("End/{rentId}")]
        [Authorize]
        public async Task<IActionResult> Finish(string rentId, double Lat, double Long, CancellationToken cancellationToken)
        {
            FinishRentRequest request = new FinishRentRequest()
            {
                Lat = Lat,
                Longitude = Long,
                RentId = rentId
            };

            return await _mediator.Send(request, cancellationToken);
        }
        [HttpGet("TransportHistory/{transportId}")]
        public async Task<IActionResult> GetTransportHistory(string transportId, CancellationToken cancellationToken)
        {
            GetTransportHistoryRequest request = new GetTransportHistoryRequest()
            {
                TransportId = transportId,
                User = User
            };

            return await _mediator.Send(request, cancellationToken);
        }
    }
}
