using LMS.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Service
{
    public class JwtService
    {
        private readonly string _key;
        public JwtService(IConfiguration config)
        {
            _key = config["Jwt:Key"] 
                ??  throw new Exception("Jwt key not found!");
        }
        public string GenerateToken(AppUser user)
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms
                .HmacSha256);
            var token = new JwtSecurityToken(
             claims: Claims,
             expires: DateTime.UtcNow.AddHours(1),
             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
      }
    }

