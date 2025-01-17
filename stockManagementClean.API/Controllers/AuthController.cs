using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stockManagementClean.API.Utilities.JwtUtility;
using Serilog;
using Microsoft.Extensions.Logging;

namespace stockManagementClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            //_logger = logger;
        }
        [HttpGet]
        public IActionResult Get(string username)
        {
           var result = _tokenGenerator.GenerateToken(username);
           //_logger.LogInformation("Custom Log: User ID {username} performed {Operation} at {Timestamp}",
             //  username, "Generate Token", DateTime.UtcNow);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
