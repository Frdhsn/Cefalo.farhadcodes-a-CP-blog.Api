using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUser(int id);
        Task<User?> PostUser(User user);
        Task<User?> UpdateUser(int id, User user);
        Task<Boolean> DeleteUser(int id);
    }
}
