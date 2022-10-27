using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.farhadcodes_a_CP_blog.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPassword _passwordH;
        public AuthController(IAuthService auth, IPassword passwordH)
        {
            _authService = auth;
            _passwordH = passwordH;
        }
        [HttpPost("signup")]
        public async Task<ActionResult<UserDTO>> SignUp(SignUpDTO req)
        {
            var userDTO = await _authService.SignUp(req);

            if (userDTO == null)
                return BadRequest("Something went wrong! Can't create a new user!");

            return CreatedAtAction(nameof(SignUp), userDTO.Id, userDTO);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO req)
        {

            var userDTO = await _authService.Login(req);

            if (userDTO == null)
                return BadRequest("Something went wrong! Can't login!");

            return userDTO;
        }


    }
}
