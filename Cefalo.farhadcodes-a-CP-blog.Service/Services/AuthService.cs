using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Services
{
    public class AuthService: IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IPassword _passwordH;
        private readonly IUserRepository _userRepository;
        private readonly BaseDTOValidator<LoginDTO> _logindtovalidator;
        private readonly BaseDTOValidator<SignUpDTO> _signupdtovalidator;

        public AuthService(BaseDTOValidator<SignUpDTO> signupdtovalidator, BaseDTOValidator<LoginDTO> logindtovalidator, IUserRepository userRepository, IConfiguration config,IPassword passwordH, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _passwordH = passwordH;
            _logindtovalidator = logindtovalidator;
            _signupdtovalidator = signupdtovalidator;
        }

        public async Task<UserDTO?> SignUp(SignUpDTO req)
        {
            _signupdtovalidator.ValidateDTO(req);
            _passwordH.HashPassword(req.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = _mapper.Map<User>(req);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.LastModifiedTime = DateTime.UtcNow;
            user.CreationTime = DateTime.UtcNow;

            var newUser = await _userRepository.PostUser(user);
            var userDTO = _mapper.Map<UserDTO>(newUser);
            return userDTO;
        }
        public async Task<UserDTO> Login(LoginDTO req)
        {
            _logindtovalidator.ValidateDTO(req);

            var newUser = await _userRepository.GetUserByEmail(req.Email);
            if (newUser == null) throw new UnauthorisedHandler("Incorrect email or password!");
            bool ret = _passwordH.VerifyHash(req.Password, newUser.PasswordHash, newUser.PasswordSalt);
            if (!ret) throw new UnauthorisedHandler("Incorrect email or password!");
            var token = _passwordH.CreateToken(newUser);
            
            var userDTO = _mapper.Map<UserDTO>(newUser);
            userDTO.Token = token;
            return userDTO;
        }

    }
}
