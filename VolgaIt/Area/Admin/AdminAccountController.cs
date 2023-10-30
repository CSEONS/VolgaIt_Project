using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AdminAccountCommands.Create;
using VolgaIt.MediatR.AdminAccountCommands.Delete;
using VolgaIt.MediatR.AdminAccountCommands.Get;
using VolgaIt.MediatR.AdminAccountCommands.GetQuerable;
using VolgaIt.MediatR.AdminAccountCommands;
using VolgaIt.MediatR.AdminAccountCommands.Update;

namespace VolgaIt.Area.Admin
{
    [Route("api/Admin/Account")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminAccountController : ControllerBase
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public AdminAccountController(DataManager dataManager, UserManager<AppUser> userManager, IMediator mediator)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts(int start, int count, CancellationToken cancellationToken) //TODO: Изменить возвращаемый тип на VeiwModel и добавить область только для админов. Понять как будуь передоваться параметры start & count
        {
            GetQuerableAccountRequest request = new GetQuerableAccountRequest()
            {
                Count = count,
                Start = start,
            };

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(string id)
        {
            GetAccountRequest request = new GetAccountRequest()
            {
                Id = id
            };

            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            request.AccountId = id;

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            DeleteAccountRequest request = new DeleteAccountRequest()
            {
                AccountId = id
            };

            return await _mediator.Send(request, cancellationToken);
        }
    }
}
