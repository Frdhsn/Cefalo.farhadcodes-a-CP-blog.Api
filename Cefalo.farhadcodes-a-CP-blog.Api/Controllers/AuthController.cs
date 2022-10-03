using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.farhadcodes_a_CP_blog.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthController(IUserService user,IMapper mapper, IAuthService auth, IConfiguration configuration)
        {
            _userService = user;
            _mapper = mapper;
            _authService = auth;
            _configuration = configuration;
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
