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
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Services
{
    public class AuthService: IAuthService
    {
        private readonly IMapper _mapper;
        //private readonly IConfiguration _config;
        private readonly IPassword _passwordH;
        private readonly IUserRepository _userRepository;
        private readonly BaseDTOValidator<LoginDTO> _logindtovalidator;
        private readonly BaseDTOValidator<SignUpDTO> _signupdtovalidator;

        public AuthService(IUserRepository userRepository, IMapper mapper, IPassword passwordH, BaseDTOValidator<LoginDTO> logindtovalidator, BaseDTOValidator<SignUpDTO> signupdtovalidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            //_config = config;
            _passwordH = passwordH;
            _logindtovalidator = logindtovalidator;
            _signupdtovalidator = signupdtovalidator;
        }

        public async Task<UserDTO> SignUp(SignUpDTO req)
        {
            _signupdtovalidator.ValidateDTO(req);

            Tuple<byte[], byte[]> hashedPassword= _passwordH.HashPassword(req.Password);

            var user = _mapper.Map<User>(req);
            user.PasswordHash = hashedPassword.Item1;
            user.PasswordSalt = hashedPassword.Item2;
            user.LastModifiedTime = DateTime.UtcNow;
            user.CreationTime = DateTime.UtcNow;

            var newUser = await _userRepository.PostUser(user);
            var userDTO = _mapper.Map<UserDTO>(newUser);
            userDTO.Token = _passwordH.CreateToken(newUser);
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
