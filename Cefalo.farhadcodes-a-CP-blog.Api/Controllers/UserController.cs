using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.farhadcodes_a_CP_blog.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return BadRequest("User not found!");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserDTO request)
        {
            var userDto = await _userService.PostUser(request);
            if (userDto == null)
                return BadRequest("Something went wrong! Can't create a new user!");

            return CreatedAtAction(nameof(PostUser), userDto.Id, userDto);
            //return CreatedAtAction(nameof(PostUser), userDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO updatedUserDto)
        {
            if (id != updatedUserDto.Id)
                return BadRequest("Something went wrong! Can't update the user.");

            var updatedDTO = await _userService.UpdateUser(id, updatedUserDto);
            
            if (updatedDTO == null)
                return BadRequest("Something went wrong! Can't update the user.");
            return Ok(updatedDTO);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _userService.DeleteUser(id);
            if (!deletedUser) throw new BadRequestHandler("Something went wrong! Can't delete the user.");
            return NoContent();
        }
    }
}
