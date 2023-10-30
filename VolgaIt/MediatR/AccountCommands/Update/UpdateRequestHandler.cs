using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
using System.Text.Json;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;
using VolgaIt.Service.Interfaces;

namespace VolgaIt.MediatR.AccountCommands.Update
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public UpdateRequestHandler(UserManager<AppUser> userManager, IDatabaseService databaseService, IMapper mapper)
        {
            _userManager = userManager;
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(UpdateRequest request, CancellationToken cancellationToken)
        {
            var updatingUser = await _userManager.GetUserAsync(request.User);

            if (updatingUser is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            if (await _userManager.ExistByName(request.Username) && updatingUser.UserName != request.Username)
                return new BadRequestObjectResult(new { error = ActionMessages.UsernameTaken(request.Username) });


            await _databaseService.BeginTransactionAsync();

            try
            {
                var usernameChangeResult = await _userManager.SetUserNameAsync(updatingUser, request.Username);
                if (usernameChangeResult.Succeeded is false)
                    throw new Exception(JsonSerializer.Serialize(usernameChangeResult.Errors));

                if (string.IsNullOrEmpty(request.NewPassword) is false)
                {
                    var passwordDeleteResult = await _userManager.RemovePasswordAsync(updatingUser);
                    if (passwordDeleteResult.Succeeded is false)
                        throw new Exception(JsonSerializer.Serialize(passwordDeleteResult));

                    var passwordSetResult = await _userManager.AddPasswordAsync(updatingUser, request.NewPassword);
                    if (passwordSetResult.Succeeded is false)
                        throw new Exception(JsonSerializer.Serialize(passwordSetResult));
                };

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
