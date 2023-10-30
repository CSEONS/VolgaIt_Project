using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.RentCommands.BeginCommands
{
    public class BeginRentRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string? TransportId { get; set; }
        [JsonIgnore]
        public ClaimsPrincipal? User { get; set; }
        public string Type { get; set; }
    }
}
