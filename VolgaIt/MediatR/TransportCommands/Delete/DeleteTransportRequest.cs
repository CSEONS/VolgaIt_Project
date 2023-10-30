using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.TransportCommands.Delete
{
    public class DeleteTransportRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string? TransportId { get; set; }
        [JsonIgnore]
        public ClaimsPrincipal? From { get; set; }
    }
}
