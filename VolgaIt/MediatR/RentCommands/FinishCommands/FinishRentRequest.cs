using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.RentCommands.FinishCommands
{
    public class FinishRentRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string? RentId { get; set; }
        [JsonIgnore]
        public double? Lat { get; set; }
        [JsonIgnore]
        public double? Longitude { get; set; }
    }
}
