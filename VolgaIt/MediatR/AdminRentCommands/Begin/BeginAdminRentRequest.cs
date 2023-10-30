using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminRentCommands.Begin
{
    public class BeginAdminRentRequest :IRequest<IActionResult>
    {
        public string UserId { get; set; }
        public string TransportId { get; set; }
        public string PriceType { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public double PriceOfUnit { get; set; }
        public double FinalPrice { get; set; }
    }
}
