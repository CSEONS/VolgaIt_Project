using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminTransportCommands.GetQuerable
{
    public class GetQuarableAdminTransportRequest : IRequest<IActionResult>
    {
        public int Start {  get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
    }
}
