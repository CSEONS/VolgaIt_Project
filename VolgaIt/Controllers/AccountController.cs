using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.MediatR.AccountCommands.GetMyself;
using VolgaIt.MediatR.AccountCommands.Login;
using VolgaIt.MediatR.AccountCommands.Logout;
using VolgaIt.MediatR.AccountCommands.Register;
using VolgaIt.MediatR.AccountCommands.Update;

namespace VolgaIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<IActionResult> GetMyself(CancellationToken cancellationToken)
        {
            GetMyselfRequest request = new GetMyselfRequest()
            {
                User = User
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("SignOut")]
        [Authorize]
        public async Task<IActionResult> Lgout(CancellationToken cancellationToken)
        {
            LogoutRequest request = new LogoutRequest()
            {
                User = User
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateRequest request, CancellationToken cancellationToken)
        {
            request.User = User;
            //TODO: Проверить можно ли замаппить через запрос свойство которон отмечего JSONIgnore

            return await _mediator.Send(request, cancellationToken);
        }
    }
}
