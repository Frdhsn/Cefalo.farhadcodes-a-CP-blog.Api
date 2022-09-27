using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPassword _passwordH;
        private readonly IUserRepository _userRepository;
        private readonly IJWTToken _jwtTokenHandler;
        public UserService(IUserRepository userRepository,IPassword passwordH, IMapper mapper, IJWTToken jwtTokenHandler) { 
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordH = passwordH;
            _jwtTokenHandler = jwtTokenHandler;
        }

        
         public async Task<List<UserDTO>> GetAllUsers(){
                var users = await _userRepository.GetAllUsers();
                return users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
            }
        public async Task<UserDTO?> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO?> PostUser(UserDTO request)
        {
            var user = _mapper.Map<User>(request);
            user.LastModifiedTime = DateTime.UtcNow;
            user.CreationTime = DateTime.UtcNow;
            var newUser = await _userRepository.PostUser(user);
            var userDto = _mapper.Map<UserDTO>(newUser);
            return userDto;
        }

        public async Task<UserDTO?> UpdateUser(int id, UserDTO updateUserDto)
        {
            //if (id != updateUserDto) return null;
            User mappedUser = _mapper.Map<User>(updateUserDto);
            
            var updatedUser = await _userRepository.UpdateUser(id, mappedUser);
            if (updatedUser == null) return null;
            
            var userDto = _mapper.Map<UserDTO>(updatedUser);
            return userDto;
        }
        public async Task<Boolean> DeleteUser(int id)
        {
            //var fetchedUser = await _userRepository.GetUser(id);

            //if (fetchedUser == null) throw new NotFoundHandler("User NOT FOUND!");

            //var currUser = _jwtTokenHandler.GetLoggedInUser();
            //if (currUser != fetchedUser.Name) throw new UnauthorisedHandler("Not authorized!");

            //var creationTime = _jwtTokenHandler.GetTokenCreationTime();
            //if (creationTime == null) throw new UnauthorisedHandler("Login again");

            //DateTime tokenCreationTime = Convert.ToDateTime(creationTime);

            //if (DateTime.Compare(tokenCreationTime, fetchedUser.LastModifiedTime) < 0)
            //    throw new UnauthorisedHandler("Login again!");

            //return await _userRepository.DeleteUser(id);
            var fetchedUser = await _userRepository.GetUser(id);

            if (fetchedUser == null) throw new NotFoundHandler("User NOT FOUND!");

            //var currUser = _jwtTokenHandler.GetLoggedInUser();
            //if (currUser != fetchedUser.Name) throw new UnauthorisedHandler("Not authorized!");

            //var creationTime = _jwtTokenHandler.GetTokenCreationTime();
            //if (creationTime == null) throw new UnauthorisedHandler("Login again");

            //DateTime tokenCreationTime = Convert.ToDateTime(creationTime);

            //if (DateTime.Compare(tokenCreationTime, fetchedUser.LastModifiedTime) < 0)
            //    throw new UnauthorisedHandler("Login again!");

            return await _userRepository.DeleteUser(id);
        }
    }
}
