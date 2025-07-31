using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TalabatProject.Core.Entity.Identity;
using TalabatProject.Repository.Identity;
using TalabatProject.Service;

namespace Talabat.APIs.Extensions
{
    public static class IdentityServicesExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration config)
        {
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;               
            }).AddEntityFrameworkStores<AppIdentityContext>(); 
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(optoins =>
                   optoins.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = config["JWT:ValidIssure"],
                       ValidateAudience = true,
                       ValidAudience = config["JWT:ValidAudiance"],
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWt:Key"])),
                   }
                );
            return Services;
        }

    }
}
