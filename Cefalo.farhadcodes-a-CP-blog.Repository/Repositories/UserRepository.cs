using Cefalo.farhadcodes_a_CP_blog.Database.Context;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Repository.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly CPContext _context;
        public UserRepository(CPContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> UpdateUser(int id,User user)
        {
            var updatedUser = await _context.Users.FindAsync(id);

            if(updatedUser == null)
            {
                return null;
            }
            updatedUser.Name = user.Name;
            updatedUser.Email = user.Email;
            updatedUser.LastModifiedTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return updatedUser;
        }
        public async Task<Boolean> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
