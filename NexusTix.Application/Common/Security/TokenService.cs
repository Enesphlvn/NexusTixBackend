using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NexusTix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NexusTix.Application.Common.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string token, DateTime expiration) GenerateToken(User user, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("FullName", $"{user.FirstName} {user.LastName}")
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: expiration,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }
    }
}
