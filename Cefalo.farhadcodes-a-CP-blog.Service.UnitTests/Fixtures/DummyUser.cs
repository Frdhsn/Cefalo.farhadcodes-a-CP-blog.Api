using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Fixtures
{
    public class DummyUser
    {

        public User dummyUser,dummyUser2;
        public List<User> dummyUserList;
        public UserDTO dummyUserDTO, dummyUserDTO2;
        public List<UserDTO> dummyUserDTOList;
        
        public SignUpDTO dummySignUpDTO;
        public LoginDTO dummyLoginDTO;
        public DummyUser()
        {
            dummyUser = A.Fake<User>(y => y.WithArgumentsForConstructor(() => new User()));
            dummyUser.Id = 1;
            dummyUser.Name = "Messi";
            dummyUser.Email = "messi@gmail.com";

            dummyUser.PasswordHash = new byte[10];
            dummyUser.PasswordSalt = new byte[5];
            dummyUser.CreationTime = DateTime.Now;
            dummyUser.LastModifiedTime = DateTime.Now;

            dummyUser2 = A.Fake<User>(y => y.WithArgumentsForConstructor(() => new User()));
            dummyUser2.Id = 2;
            dummyUser2.Name = "Ronaldo";
            dummyUser2.Email = "ronaldo@gmail.com";

            dummyUser2.PasswordHash = new byte[10];
            dummyUser2.PasswordSalt = new byte[5];
            dummyUser2.CreationTime = DateTime.Now;
            dummyUser2.LastModifiedTime = DateTime.Now;


            dummyUserList = new List<User>();
            dummyUserList.Add(dummyUser);
            dummyUserList.Add(dummyUser2);

            dummyUserDTO = A.Fake<UserDTO>(y => y.WithArgumentsForConstructor(() => new UserDTO()));
            dummyUserDTO.Id = 1;
            dummyUserDTO.Name = "Messi";
            dummyUserDTO.Email = "messi@gmail.com";
            dummyUserDTO.Token = "brokenheartedhooverfixersuckerguy";
            dummyUserDTO.CreationTime = DateTime.Now;
            dummyUserDTO.LastModifiedTime = DateTime.Now;

            dummyUserDTO2 = A.Fake<UserDTO>(y => y.WithArgumentsForConstructor(() => new UserDTO()));
            dummyUserDTO2.Id = 2;
            dummyUserDTO2.Name = "Ronaldo";
            dummyUserDTO2.Email = "ronaldo@gmail.com";
            dummyUserDTO2.Token = "brokenheartedhooverfixersuckerguy";
            dummyUserDTO2.CreationTime = DateTime.Now;
            dummyUserDTO2.LastModifiedTime = DateTime.Now;


            dummyUserDTOList = new List<UserDTO>();
            dummyUserDTOList.Add(dummyUserDTO);
            dummyUserDTOList.Add(dummyUserDTO2);

            dummyLoginDTO = A.Fake<LoginDTO>(x => x.WithArgumentsForConstructor(() => new LoginDTO()));
            dummyLoginDTO.Email = "messi";
            dummyLoginDTO.Password = "messi12345";


            dummySignUpDTO = A.Fake<SignUpDTO>(x => x.WithArgumentsForConstructor(() => new SignUpDTO()));
            dummySignUpDTO.Name = "messi";
            dummySignUpDTO.Email = "messi@gmail.com";
            dummySignUpDTO.Password = "messi12345";
        }
    }
}
