using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.RentCommands.GetLoactionTransportsCommands
{
    public class GetLoactionTransportsRequest : IRequest<IActionResult>
    {
        public double Lat {  get; set; }
        public double Long { get; set; }
        public double Radius { get; set; }
        public string Type { get; set; }
    }
}
