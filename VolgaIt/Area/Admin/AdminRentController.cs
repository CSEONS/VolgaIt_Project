using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.MediatR.AdminRentCommands.Begin;
using VolgaIt.MediatR.AdminRentCommands.Delete;
using VolgaIt.MediatR.AdminRentCommands.Finish;
using VolgaIt.MediatR.AdminRentCommands.GetAdminRentDetails;
using VolgaIt.MediatR.AdminRentCommands.GetTransportHistory;
using VolgaIt.MediatR.AdminRentCommands.GetUserHistory;
using VolgaIt.MediatR.AdminRentCommands.Update;
using VolgaIt.MediatR.RentCommands.GetTransportHistoryCommands;

namespace VolgaIt.Area.Admin
{
    [Route("api/Admin/Rent")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminRentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminRentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{rentId}")]
        public async Task<IActionResult> GetDetais(string rentId, CancellationToken cancellationToken)
        {
            GetAdminRentDetailsRequest request = new GetAdminRentDetailsRequest()
            {
                RentId = rentId
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpGet("UserHistory/{userId}")]
        public async Task<IActionResult> GetUserHistory(string userId, CancellationToken cancellationToken)
        {
            GetAdminUserHistoryRequest request = new GetAdminUserHistoryRequest()
            {
                UserId = userId
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpGet("TransportHistory/{transportId}")]
        public async Task<IActionResult> GetTransportHistory(string transportId, CancellationToken cancellationToken)
        {
            GetAdminTransportHistoryRequest request = new GetAdminTransportHistoryRequest()
            {
                TransportId = transportId
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> BeginRent(BeginAdminRentRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("End/{rentId}")]
        public async Task<IActionResult> FinishRent(string rentId, [FromQuery] double Lat, [FromQuery] double Long, CancellationToken cancellationToken)
        {
            FinishAdminRentRequest request = new FinishAdminRentRequest()
            {
                RentId = rentId,
                Lat = Lat,
                Long = Long,
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRent(string id, UpdateAdminRentRequest request, CancellationToken cancellationToken)
        {
            request.RentId = id;

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{rentId}")]
        public async Task<IActionResult> DeleteRent(string rentId, CancellationToken cancellationToken)
        {
            DeleteAdminRentRequest request = new DeleteAdminRentRequest()
            {
                RentId = rentId,
            };

            return await _mediator.Send(request, cancellationToken);
        }
    }
}
