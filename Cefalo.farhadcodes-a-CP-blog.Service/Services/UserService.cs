using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper) { 
            _userRepository = userRepository;
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
            if (id != updateUserDto.Id) return null;
            User mappedUser = _mapper.Map<User>(updateUserDto);
            
            var updatedUser = await _userRepository.UpdateUser(id, mappedUser);
            if (updatedUser == null) return null;
            
            var userDto = _mapper.Map<UserDTO>(updatedUser);
            return userDto;
        }
        public Task<Boolean> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
    }
}
