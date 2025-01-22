using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using stockManagement.Application.Interfaces;
using stockManagement.Models.Entity;
using stockManagement.Models.StockDTO.UsersDTO;
namespace stockManagementClean.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAllOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper =  mapper;
        }
        [HttpGet]
        public IActionResult GetUser(string username)
        {
            var result = _userService.GetUser(username);
            if (result != null)
            {
                return Ok(result);  
            }
            return BadRequest(result.errorMessage);
        }
        [HttpPost]
        public IActionResult CreateUser(AddUpdateUserRequestDTO userdto)
        {
            var UsersToAdd = _mapper.Map<Users>(userdto);
            var result = _userService.CreateUsersAsync(UsersToAdd);
            var resposeDTO = _mapper.Map<AddCreateUsersResponse>(result.Result.Data);
            if (result.Result.isSuccessfullyCompleted)
            {
                return Ok(resposeDTO);
            }
            return BadRequest(result.Result.errorMessage);
        }
        [HttpPost("/bulk")]
        public IActionResult CreateBulkUser(List<AddUpdateUserRequestDTO> users)
        {
            List<Users> UsersToAdd = new List<Users>();
            foreach (AddUpdateUserRequestDTO user in users)
            {
                var convertedUser = _mapper.Map<Users>(user);
                UsersToAdd.Add(convertedUser);
            }
            var result = _userService.CreateBulkUsersAsync(UsersToAdd);
            if (result.Result.isSuccessfullyCompleted)
            {
                return Ok(result);
            }
            return BadRequest(result.Result.errorMessage);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(AddUpdateUserRequestDTO user) 
        {
            var UserToAdd = _mapper.Map<Users>(user);
            var result = await _userService.DeleteUsersAsync(UserToAdd);
            if (result.isSuccessfullyCompleted)
            {
                return Ok(result);
            }
            return NotFound("Error:" + result.errorMessage);
        }
        [HttpPut]
        public async Task<IActionResult> UpDateUser(AddCreateUsersResponse user)
        {
            var UserToUpdate = _mapper.Map<Users>(user);
            var result = await _userService.UpdateUsersAsync(UserToUpdate);
            if (result.isSuccessfullyCompleted)
            {
                return Ok(result);
            }
            return NotFound("Error:" + result.errorMessage);
        }
    }
}
