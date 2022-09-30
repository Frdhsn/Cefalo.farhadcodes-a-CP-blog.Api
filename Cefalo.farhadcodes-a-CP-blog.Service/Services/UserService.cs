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
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.TechDaily.Service.DtoValidators;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPassword _passwordH;
        private readonly IUserRepository _userRepository;
        private readonly BaseDTOValidator<UserDTO> _userdtovalidator;
        public UserService(IUserRepository userRepository,IPassword passwordH, IMapper mapper, BaseDTOValidator<UserDTO> userdtovalidator) { 
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordH = passwordH;
            _userdtovalidator = userdtovalidator;
        }

        
         public async Task<List<UserDTO>> GetAllUsers(){
                var users = await _userRepository.GetAllUsers();
                return users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
            }
        public async Task<UserDTO?> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null) throw new NotFoundHandler("No user was found with that ID!");
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null) throw new NotFoundHandler("No user was found with that email!");
            return _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO?> PostUser(UserDTO request)
        {
            _userdtovalidator.Validate(request);

            var user = _mapper.Map<User>(request);
            user.LastModifiedTime = DateTime.UtcNow;
            user.CreationTime = DateTime.UtcNow;
            var newUser = await _userRepository.PostUser(user);
            var userDto = _mapper.Map<UserDTO>(newUser);
            return userDto;
        }

        public async Task<UserDTO?> UpdateUser(int id, UserDTO updateUserDto)
        {
            _userdtovalidator.Validate(updateUserDto);

            var currUserId = _passwordH.GetLoggedInId();
            if(currUserId == -1) throw new UnauthorisedHandler("You're not logged in! Please log in to get access.");
            if (currUserId != id) throw new ForbiddenHandler("You don't have the permission!");

            var fetchedUser = await _userRepository.GetUser(id);

            if (fetchedUser == null) throw new NotFoundHandler("No user was found with that ID!");
            
            var creationTime = _passwordH.GetTokenCreationTime();
            if (creationTime == null) throw new UnauthorisedHandler("You're not logged in! Please log in to get access.");

            DateTime tokenCreationTime = Convert.ToDateTime(creationTime);

            if (DateTime.Compare(tokenCreationTime, fetchedUser.LastModifiedTime) < 0)
                throw new UnauthorisedHandler("JWT Expired! Login again!");

            User mappedUser = _mapper.Map<User>(updateUserDto);

            
            var updatedUser = await _userRepository.UpdateUser(id, mappedUser);

            if (updatedUser == null) throw new NotFoundHandler("No user was found with that ID!");

            var userDto = _mapper.Map<UserDTO>(updatedUser);
            return userDto;
        }
        public async Task<Boolean> DeleteUser(int id)
        {
            var fetchedUser = await _userRepository.GetUser(id);

            if (fetchedUser == null) throw new NotFoundHandler("No user was found with that ID!");
            var currUserId = _passwordH.GetLoggedInId();
            if (currUserId != id) throw new ForbiddenHandler("You don't have the permission!");

            var creationTime = _passwordH.GetTokenCreationTime();
            if (creationTime == null) throw new UnauthorisedHandler("You're not logged in! Please log in to get access.");

            DateTime tokenCreationTime = Convert.ToDateTime(creationTime);

            if (DateTime.Compare(tokenCreationTime, fetchedUser.LastModifiedTime) < 0)
                throw new UnauthorisedHandler("JWT Expired! Login again!");
            return await _userRepository.DeleteUser(id);
        }
    }
}
