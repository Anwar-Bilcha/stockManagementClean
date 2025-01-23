using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stockManagement.Application.Interfaces;
using stockManagement.Models.StockDTO.UsersDTO;
using stockManagementClean.API.utilities;

namespace stockManagementClean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IUserService userService, ITokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }
        [HttpPost]
        public IActionResult GenerateToken([FromBody]AddUpdateUserRequestDTO user)
        {
            var userfromTable = _userService.GetUser(user.UserName);
            if (userfromTable.Data != null && userfromTable.Data.Password == user.Password)
            {
               var result = _tokenGenerator.GenerateToken(user, userfromTable.Data.UserRole);
                return Ok(result);
            }
            return NotFound($"user with {user.UserName} is not registered");
        }
    }
}
