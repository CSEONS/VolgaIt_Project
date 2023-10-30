using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Net;
using System.Text.Json;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.Service;
using VolgaIt.Service.Extensions;
using VolgaIt.Service.Interfaces;

namespace VolgaIt.MediatR.AccountCommands.Register
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public RegisterRequestHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IDatabaseService databaseService, IMapper mapper)
        {    
            _userManager = userManager;
            _signInManager = signInManager;
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _userManager.ExistByName(request.Username))
                return new BadRequestObjectResult(new { error = ActionMessages.UsernameTaken(request.Username) });

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return new BadRequestObjectResult(new { error = ActionMessages.InvalidCredintials() });

            await _databaseService.BeginTransactionAsync();

            AppUser registeringUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            try
            {
                var createingUserResult = await _userManager.CreateAsync(registeringUser);
                if (createingUserResult.Succeeded is false)
                    throw new Exception(JsonSerializer.Serialize(createingUserResult));

                var setPasswordResult = await _userManager.AddPasswordAsync(registeringUser, request.Password);
                if (setPasswordResult.Succeeded is false)
                    throw new Exception(JsonSerializer.Serialize(setPasswordResult));

                await _databaseService.CommitAsync();
            }
            catch (Exception ex)
            {
                await _databaseService.RollbackAsync();
                return new BadRequestObjectResult(new { error = ex.Message });
            }



            var userViewModel = _mapper.Map<AppUser, AppUserViewModel>(registeringUser);

            return new OkObjectResult(new { message = ActionMessages.UserCreated(), user =  userViewModel });
        }
    }
}
