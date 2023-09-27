using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyAuth2._0.IServices;
using MyAuth2._0.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace MyAuth2._0.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly Jwt _jwt;
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtTokenGenerator(IOptions<Jwt> jwt, UserManager<ApplicationUser> userManager)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
        }
        public string GenerateToken(ApplicationUser applicationUser)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Key);
            var roles = _userManager.GetRolesAsync(applicationUser).Result.ToList();
            string userRoles = "";
            foreach (string rname in roles)
            {
                if (userRoles.Trim().Length == 0)
                    userRoles = rname;
                else
                    userRoles = userRoles + "," + rname;
            }

            var claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName),
                  new Claim(ClaimTypes.Role,userRoles),


            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience=_jwt.Audience,
                Issuer=_jwt.Issuer,
                Subject=new ClaimsIdentity(claimsList),
                Expires=DateTime.UtcNow.AddDays(2),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}
