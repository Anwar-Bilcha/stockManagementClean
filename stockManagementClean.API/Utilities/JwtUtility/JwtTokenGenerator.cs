using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace stockManagementClean.API.Utilities.JwtUtility
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username)
        {
            StockJwtConfiguration.Initialize(_configuration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StockJwtConfiguration.JwtSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            // Add other claims as needed (e.g., roles, permissions)
             new Claim(ClaimTypes.Role, "Admin") 
        };

            var token = new JwtSecurityToken(
                issuer: StockJwtConfiguration.JwtIssuer,
                audience: StockJwtConfiguration.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(StockJwtConfiguration.tokenExpirationMinutes), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username);
    }
}
