using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Client.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        public TokenBuilder(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Task<Token> GetToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"]));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: Configuration["AuthSettings:Issuer"],
                audience: Configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var token = new Token
            {
                ValidTo = jwtSecurityToken.ValidTo,
                TokenAsString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return Task.FromResult(token);
        }
    }
}
