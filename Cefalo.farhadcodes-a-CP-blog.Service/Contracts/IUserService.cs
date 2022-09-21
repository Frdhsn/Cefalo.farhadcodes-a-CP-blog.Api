using Cefalo.farhadcodes_a_CP_blog.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Contracts
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO?> GetUser(int id);
        Task<UserDTO?> PostUser(UserDTO user);
        Task<UserDTO?> UpdateUser(int id,UserDTO user);
        Task<Boolean> DeleteUser(int id);

    }
}
