using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AccountCommands.Login
{
    public class LoginRequest : IRequest<IActionResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
