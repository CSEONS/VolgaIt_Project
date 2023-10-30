using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;

namespace VolgaIt.MediatR.AdminAccountCommands.Create
{
    public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateAccountRequestHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            if (await _userManager.ExistByName(request.Username))
                return new BadRequestObjectResult(new { error = ActionMessages.UsernameTaken(request.Username) });

            var user = new AppUser()
            {
                UserName = request.Username,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, request.Password),
                Balance = request.Balance,
            };
            await _userManager.CreateAsync(user);

            if (request.IsAdmin)
                await _userManager.AddToRoleAsync(user, "Admin");

            return new OkObjectResult(new { message = ActionMessages.UserCreated(), user });
        }
    }
}
