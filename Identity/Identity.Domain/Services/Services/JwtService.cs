using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Module.Identity.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Module.Identity.Domain.Services.Services
{
    public class JwtService(IConfiguration _config) : IJwtService
    {
        public string GenerateToken(int userId, string role)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
        };
            // Add role/s
            
            

            claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
