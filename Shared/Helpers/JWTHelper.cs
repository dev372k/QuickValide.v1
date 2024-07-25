using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared.DTOs.UserDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Helpers
{
    public class JWTHelper
    {
        public static string CreateToken(GetUserDTO user)
        {
            Appsettings appsettings = Appsettings.Instance;
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Email),
                //new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                appsettings.GetValue("SecretKeys:JWT")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

