using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts
{
    public interface IPassword
    {

        void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt);

        string CreateToken(User user);
    }
}
