using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AccountCommands.Logout
{
    public class LogoutRequestHandler : IRequestHandler<LogoutRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LogoutRequestHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(request.User);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            user.AccesToken = string.Empty;
            user.RefreshToken = string.Empty;

            await _userManager.UpdateAsync(user);

            await _signInManager.SignOutAsync();

            return new OkObjectResult(new { message = ActionMessages.SignOut() });
        }
    }
}
