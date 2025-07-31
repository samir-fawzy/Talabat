using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TalabatProject.Core.Entity.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal currenUser)
        {
            var email = currenUser.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u => u.Address)
                            .FirstOrDefaultAsync(e => e.Email == email);
            return user;
        }
        public static async Task<bool> CheckEmailExist(this UserManager<AppUser> userManager,string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
