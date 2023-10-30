using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VolgaIt.MediatR.AccountCommands.Logout
{
    public class LogoutRequest : IRequest<IActionResult>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
