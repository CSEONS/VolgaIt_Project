using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Models;

namespace VolgaIt.MediatR.TransportCommands.Get
{
    public class GetTransportRequest : IRequest<IActionResult>
    {
        public string Id { get; set; }
    }
}
