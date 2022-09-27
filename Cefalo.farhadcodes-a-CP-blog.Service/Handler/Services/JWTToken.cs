using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services
{
    public class JWTToken: IJWTToken
    {

        //Task<string> GetLoggedInUser()
        //{
            //UserDTO userDTO = new UserDTO();
          //  return "ok";
        //}

       string IJWTToken.GetLoggedInUser()
        {
            throw new NotImplementedException();
        }

        //Task<string> GetTokenCreationTime()
        //{
        //    string token = "time";
        //    return token;
        //}

        string IJWTToken.GetTokenCreationTime()
        {
            throw new NotImplementedException();
        }
    }
}
