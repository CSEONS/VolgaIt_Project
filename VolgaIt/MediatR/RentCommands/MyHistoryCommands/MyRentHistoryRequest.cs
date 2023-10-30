using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.RentCommands.MyHistoryCommands
{
    public class MyRentHistoryRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public ClaimsPrincipal User { get; set; }
    }
}
