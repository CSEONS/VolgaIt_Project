using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.RentCommands.GetDetailsCommands
{
    public class GetRentDetailsRequest : IRequest<IActionResult>
    {
        public string Id { get; set; }
        [JsonIgnore]
        public ClaimsPrincipal? User { get; set; }
    }
}
