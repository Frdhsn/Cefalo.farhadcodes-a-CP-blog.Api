using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Contracts
{
    public interface IAuthService
    {
        Task<string?> Login(LoginDTO req);
        Task<UserDTO?> SignUp(SignUpDTO req);
    }
}
