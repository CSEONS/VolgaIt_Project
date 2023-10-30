using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.AccountCommands.GetMyself
{
    public class GetMyselfRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public ClaimsPrincipal User { get; set; }
    }
}
