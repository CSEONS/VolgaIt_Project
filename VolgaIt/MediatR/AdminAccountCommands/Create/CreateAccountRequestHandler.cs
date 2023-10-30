using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;
using VolgaIt.Service.Interfaces;

namespace VolgaIt.MediatR.AdminAccountCommands.Create
{
    public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDatabaseService _databaseService;

        public CreateAccountRequestHandler(UserManager<AppUser> userManager, IDatabaseService databaseService)
        {
            _userManager = userManager;
            _databaseService = databaseService;
        }

        public async Task<IActionResult> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            if (await _userManager.ExistByName(request.Username))
                return new BadRequestObjectResult(new { error = ActionMessages.UsernameTaken(request.Username) });

            AppUser user = null;

            await _databaseService.BeginTransactionAsync();

            var error = new object();

            try
            {
                user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = request.Username,
                    Balance = request.Balance,
                };

                var userCretaeResult = await _userManager.CreateAsync(user);
                if (userCretaeResult.Succeeded is false)
                {
                    error = userCretaeResult.Errors;
                    throw new Exception();
                }

                if (request.IsAdmin)
                {
                    var addToAdminRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
                    if (addToAdminRoleResult.Succeeded is false)
                    {
                        error = addToAdminRoleResult.Errors;
                        throw new Exception();
                    }
                }

                var addPasswordResult = await _userManager.AddPasswordAsync(user, request.Password);
                if (addPasswordResult.Succeeded is false)
                {
                    error = addPasswordResult.Errors;
                    throw new Exception();
                }

                await _databaseService.CommitAsync();
            }
            catch (Exception)
            {
                await _databaseService.RollbackAsync();
                return new BadRequestObjectResult(error);
            }

            

            

            return new OkObjectResult(new { message = ActionMessages.UserCreated(), user });
        }
    }
}
