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
using TaskManager.SharedLayer.ResponseModels;

namespace Module.Identity.Domain.Services.Services
{
    public class JwtService(IConfiguration _config) : IJwtService
    {
        public string GenerateToken(JwtTokenData userInfoDTO)
        {
            var claims = new List<Claim>
        {

            new Claim(ClaimTypes.NameIdentifier, userInfoDTO.Id.ToString()),
            new Claim(ClaimTypes.Role, userInfoDTO.Role),
            
        };
          


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
