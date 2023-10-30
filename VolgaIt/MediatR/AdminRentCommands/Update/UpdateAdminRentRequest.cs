using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.AdminRentCommands.Update
{
    public class UpdateAdminRentRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string? RentId { get; set; }
        public string UserId { get; set; }
        public string PriceType { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public double PriceOfUnit { get; set; }
        public double FinalPrice { get; set; }
    }
}
