using OAuthTest.Config;
using OAuthTest.Interfaces;
using OAuthTest.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthTest.Services
{
    public class TokenService : ITokenService
    {
        public JwtConfig JwtConfig { get; private set; }
        public SymmetricSecurityKey SymmetricKey { get; private set; }

        public TokenService(IOptions<JwtConfig> jwtOpts)
        {
            JwtConfig = jwtOpts.Value;
            SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Secret));
        }

        public string GenerateToken(Customer customer)
        {
            var authClaims = new List<Claim>
            {
                new Claim(Constants.ClaimTypes.Id, customer.Id),
                new Claim(Constants.ClaimTypes.Subject, customer.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: JwtConfig.ValidIssuer,
                expires: DateTime.Now.AddSeconds(JwtConfig.Lifetime),
                claims: authClaims,
                signingCredentials: new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
