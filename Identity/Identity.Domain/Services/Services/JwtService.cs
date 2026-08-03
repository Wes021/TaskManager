using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Module.Identity.Domain.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.ResponseModels;

namespace Module.Identity.Domain.Services.Services
{
    public class JwtService(IConfiguration _config) : IJwtService
    {
        public string GenerateResetPasswordToken(int userId)
        {
            var key = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(_config["ResetPasswordJwt:Key"]));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim("purpose", ((int)SystemEnums.PolicyKeywords.PasswordReset).ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _config["ResetPasswordJwt:Issuer"],
                audience: _config["ResetPasswordJwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["ResetPasswordJwt:ExpiresInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateToken(JwtTokenData userInfoDTO)
        {
            var claims = new List<Claim>
        {

            new Claim(ClaimTypes.NameIdentifier, userInfoDTO.Id.ToString()),
            new Claim(ClaimTypes.Role, userInfoDTO.Role),
            new Claim(ClaimTypes.Name, userInfoDTO.FullName)

        };



            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"])),
            signingCredentials: creds
        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
