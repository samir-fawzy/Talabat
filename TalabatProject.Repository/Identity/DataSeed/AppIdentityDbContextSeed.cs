using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity.Identity;

namespace TalabatProject.Repository.Identity.DataSeed
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager,AppIdentityContext context)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "samir fawzy",
                    Email = "samir_fawzy@gmail.com",
                    UserName = "samirfawzy",
                    PhoneNumber = "0123456789"
                };
                await userManager.CreateAsync(user,"P@ssw0rd");
                var address = new Address()
                {
                    FirstName = "samir",
                    LastName = "fawzy",
                    Country = "egypy",
                    City = "cairo",
                    Street = "elmoez",
                    AppUserId = user.Id,
                    AppUser = user
                };
                await context.Addresses.AddAsync(address);
                await context.SaveChangesAsync();
            }
        }
    }
}
