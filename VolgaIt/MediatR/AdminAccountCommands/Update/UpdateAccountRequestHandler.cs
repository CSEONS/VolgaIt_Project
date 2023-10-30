using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;
using VolgaIt.Service.Interfaces;

namespace VolgaIt.MediatR.AdminAccountCommands.Update
{
    public class UpdateAccountRequestHandler : IRequestHandler<UpdateAccountRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public UpdateAccountRequestHandler(UserManager<AppUser> userManager, IMapper mapper, IDatabaseService databaseService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _databaseService = databaseService;
        }

        public async Task<IActionResult> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
        {
            var updatingUser = await _userManager.FindByIdAsync(request.AccountId);

            if (await _userManager.ExistByName(request.Username) && updatingUser.UserName != request.Username)
                return new BadRequestObjectResult(new { error = ActionMessages.UsernameTaken(request.Username) });

            if (await _userManager.ExistById(request.AccountId) is false)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            await _databaseService.BeginTransactionAsync();

            try
            {
                var usernameChangeResult = await _userManager.SetUserNameAsync(updatingUser, request.Username);
                if (usernameChangeResult.Succeeded is false)
                    throw new Exception(JsonSerializer.Serialize(usernameChangeResult.Errors));

                if (string.IsNullOrEmpty(request.Password) is false)
                {
                    var passwordDeleteResult = await _userManager.RemovePasswordAsync(updatingUser);
                    if (passwordDeleteResult.Succeeded is false)
                        throw new Exception(JsonSerializer.Serialize(passwordDeleteResult));

                    var passwordSetResult = await _userManager.AddPasswordAsync(updatingUser, request.Password);
                    if (passwordSetResult.Succeeded is false)
                        throw new Exception(JsonSerializer.Serialize(passwordSetResult));
                }

                updatingUser.Balance = request.Balance;

                await _userManager.UpdateAsync(updatingUser);

                await _databaseService.CommitAsync();

                var userViewModel = _mapper.Map<AppUser, AppUserViewModel>(updatingUser);
                return new OkObjectResult(userViewModel);
            }
            catch (Exception ex)
            {
                await _databaseService.RollbackAsync();
                return new BadRequestObjectResult(new { error = ex.Message });
            }
        }
    }
}
