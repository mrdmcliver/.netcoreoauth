using OAuthTest.Config;
using OAuthTest.Models;
using Microsoft.IdentityModel.Tokens;

namespace OAuthTest.Interfaces
{
    public interface ITokenService
    {
        SymmetricSecurityKey SymmetricKey { get; }
        JwtConfig JwtConfig { get; }

        public string GenerateToken(Customer customer);
    }
}
