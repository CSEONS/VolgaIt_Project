using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminTransportCommands.Get
{
    public class GetAdminTransportRequest : IRequest<IActionResult>
    {
        public string Id { get; set; }
    }
}
