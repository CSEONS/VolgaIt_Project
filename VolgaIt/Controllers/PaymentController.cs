using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.MediatR.PaymentCommands.Hesoyam;

namespace VolgaIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Hesoyam/{accountId}")]
        public async Task<IActionResult> Hesoyam(string accountId, CancellationToken cancellationToken)
        {
            HesoyamRequest request = new HesoyamRequest()
            {
                AccountId = accountId,
                User = User
            };

            return await _mediator.Send(request, cancellationToken);
        }
    }
}
