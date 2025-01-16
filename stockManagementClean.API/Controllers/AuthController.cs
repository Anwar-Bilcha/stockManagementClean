using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stockManagementClean.API.Utilities.JwtUtility;

namespace stockManagementClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        public AuthController(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }
        [HttpGet]
        public IActionResult Get(string username)
        {
           var result = _tokenGenerator.GenerateToken(username);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
