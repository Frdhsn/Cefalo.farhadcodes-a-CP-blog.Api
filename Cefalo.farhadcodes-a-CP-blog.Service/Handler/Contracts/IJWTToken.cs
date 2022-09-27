using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts
{
    public interface IJWTToken
    {
        string GetLoggedInUser();
        string GetTokenCreationTime();

    }
}
