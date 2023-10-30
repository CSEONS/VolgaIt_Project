using Microsoft.AspNetCore.Identity;
using VolgaIt.Domain.Entities;

namespace VolgaIt.Service.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<bool> ExistByName(this UserManager<AppUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);

            return user != null;
        }

        public static async Task<bool> ExistById(this UserManager<AppUser> userManager, string username)
        {
            var user = await userManager.FindByIdAsync(username);

            return user != null;
        }
    }
}
