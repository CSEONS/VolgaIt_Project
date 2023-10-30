using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;

namespace VolgaIt.MediatR.PaymentCommands.Hesoyam
{
    public class HesoyamRequestHandler : IRequestHandler<HesoyamRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;

        public HesoyamRequestHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(HesoyamRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(request.User);

            if(await _userManager.IsInRoleAsync(user, "Admin"))
            {
                user = await _userManager.FindByIdAsync(request.AccountId);
                user.Balance += 250000;
                await _userManager.UpdateAsync(user);            
            }
            else if(user.Id == request.AccountId)
            {
                user.Balance += 250000;
                await _userManager.UpdateAsync(user);
            }

            return new OkObjectResult(new { message = ActionMessages.Hesoyam() });
        }
    }
}
