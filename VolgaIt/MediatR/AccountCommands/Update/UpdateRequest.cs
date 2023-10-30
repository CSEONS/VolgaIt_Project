using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.AccountCommands.Update
{
    public class UpdateRequest : IRequest<IActionResult>
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        [JsonIgnore]
        public ClaimsPrincipal? User { get; set; }
    }
}
