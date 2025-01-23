using Microsoft.IdentityModel.Tokens;
using stockManagement.Models.StockDTO.UsersDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace stockManagementClean.API.utilities
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AddUpdateUserRequestDTO user, string userRole)
        {
            JwtConfiguration.Initialize(_configuration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfiguration.JwtSigningKey));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, user.UserName), new Claim(ClaimTypes.Role, userRole) };
            var jwtToken = new JwtSecurityToken(issuer: JwtConfiguration.JwtIssuer, 
                audience:  JwtConfiguration.JwtAudience, claims: claims, signingCredentials:signingCredential, expires: DateTime.Now.AddMinutes(JwtConfiguration.TokenExpirationMinutes));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }

    public interface ITokenGenerator
    {
        string GenerateToken(AddUpdateUserRequestDTO user, string userRole);
    }
}
