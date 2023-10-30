using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Create;
using VolgaIt.MediatR.TransportCommands.Delete;
using VolgaIt.MediatR.TransportCommands.Get;
using VolgaIt.MediatR.TransportCommands.Models;
using VolgaIt.MediatR.TransportCommands.Update;

namespace VolgaIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public TransportController(DataManager dataManager, UserManager<AppUser> userManager, IMediator mediator)
        { 
            _dataManager = dataManager;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken cancellationToken)
        {
             GetTransportRequest request = new GetTransportRequest()
             {
                 Id = id,
             };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Creaete([FromBody] CreateTransportRequest request, CancellationToken cancellationToken)
        {
            request.User = User;

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateTransportRequest request, CancellationToken cancellationToken)
        {
            request.UpdatedTransportId = id;

            return await _mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            DeleteTransportRequest request = new DeleteTransportRequest()
            {
                From = User,
                TransportId = id,
            };
            
            return await _mediator.Send(request, cancellationToken);
        }
    }
}
