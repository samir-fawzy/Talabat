using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity.Identity;

namespace TalabatProject.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;

        public TokenService(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var UserRoles = await userManager.GetRolesAsync(user);
            foreach (var role in UserRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            // SecurityKey
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWt:Key"]));

            var token = new JwtSecurityToken(
                issuer: config["JWT:ValidIssure"],
                audience: config["JWT:ValidAudiance"],
                expires: DateTime.Now.AddDays(double.Parse(config["JWT:DurationDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
