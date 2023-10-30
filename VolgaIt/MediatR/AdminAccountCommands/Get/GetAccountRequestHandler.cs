using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminAccountCommands.Get
{
    public class GetAccountRequestHandler : IRequestHandler<GetAccountRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetAccountRequestHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound()});

            var userViewModel = _mapper.Map<AppUser, AppUserViewModel>(user);
            return new OkObjectResult(userViewModel);
        }
    }
}
