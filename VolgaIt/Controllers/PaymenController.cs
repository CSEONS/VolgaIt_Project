using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.MediatR.PaymentCommands.Hesoyam;

namespace VolgaIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymenController(IMediator mediator)
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
