using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NGAPI.Models;

namespace NGAPI.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GetToken(AppUser user)
        {
            var tokenKey = _config["TokenKey"]?? throw new Exception("Cannot access token key from app settings");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserName)
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);

            return tokenKey ?? string.Empty;
        }
    }
}
