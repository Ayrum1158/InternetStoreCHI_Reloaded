using BLL.ConfigPOCOs;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services.AccessTokenGenerators
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly JwtConfig _jwtConfig;

        public AccessTokenGenerator(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
