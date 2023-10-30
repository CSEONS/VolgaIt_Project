using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminAccountCommands.Create
{
    public class CreateAccountRequest : IRequest<IActionResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public double Balance { get; set; }
    }
}
