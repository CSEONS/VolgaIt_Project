using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;

namespace VolgaIt.MediatR.AdminAccountCommands.Delete
{
    public class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteAccountRequestHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AccountId);

            if(user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            await _userManager.DeleteAsync(user);

            return new OkObjectResult(new { message = ActionMessages.UserDeleted()});
        }
    }
}
