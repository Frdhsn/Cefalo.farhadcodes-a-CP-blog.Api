using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;

namespace Cefalo.farhadcodes_a_CP_blog.Service
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<LoginDTO, User>();
            CreateMap<SignUpDTO, User>();
            //story
            CreateMap<StoryDTO, Story>().ReverseMap();
            CreateMap<UpdateStory, Story>();
        }

    }
}