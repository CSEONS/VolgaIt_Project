using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminRentCommands.Finish
{
    public class FinishAdminRentRequest :IRequest<IActionResult>
    {
        public string RentId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
