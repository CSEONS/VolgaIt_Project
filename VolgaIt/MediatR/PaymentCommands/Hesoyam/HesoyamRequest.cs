using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.PaymentCommands.Hesoyam
{
    public class HesoyamRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public ClaimsPrincipal? User { get; set; }
        public string? AccountId { get; set; }
    }
}
