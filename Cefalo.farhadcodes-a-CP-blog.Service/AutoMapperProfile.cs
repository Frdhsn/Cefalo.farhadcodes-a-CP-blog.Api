using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO;

namespace Cefalo.farhadcodes_a_CP_blog.Service
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();

        }

    }
}