using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AdminAccountCommands.GetQuerable;
using VolgaIt.MediatR.AdminTransportCommands.Create;
using VolgaIt.MediatR.AdminTransportCommands.Delete;
using VolgaIt.MediatR.AdminTransportCommands.Get;
using VolgaIt.MediatR.AdminTransportCommands.GetQuerable;
using VolgaIt.MediatR.AdminTransportCommands.Update;
using VolgaIt.MediatR.TransportCommands.Create;
using VolgaIt.MediatR.TransportCommands.Delete;
using VolgaIt.MediatR.TransportCommands.Get;
using VolgaIt.MediatR.TransportCommands.Update;

namespace VolgaIt.Area.Admin
{
    [Route("api/Admin/Transport")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminTransportController : ControllerBase
    {
        private readonly DataManager _dataManager;
        private readonly IMediator _mediator;

        public AdminTransportController(DataManager dataManager, IMediator mediator)
        {
            _dataManager = dataManager;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuerable([FromQuery] GetQuarableAdminTransportRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            GetAdminTransportRequest request = new GetAdminTransportRequest()
            {
                Id = id
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdminTransportRequest request, CancellationToken cancellationToken)
        {
            request.User = User;

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAdminTransportRequest request, CancellationToken cancellationToken)
        {
            request.UpdatedTransportId = id;

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            DeleteAdminTransportRequest request = new DeleteAdminTransportRequest()
            {
                From = User,
                TransportId = id
            };

            return await _mediator.Send(request, cancellationToken);
        }
    }
}
