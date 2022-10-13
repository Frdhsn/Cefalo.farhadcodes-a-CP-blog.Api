using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Fixtures
{
    public class DummyStories
    {
        public Story dummystory1;
        public Story dummystory2;
        public Story dummystory3;

        public List<Story> stories;
        public List<Story> storiesByUser;

        public StoryDTO dummyStoryDTO;
        public ShowStoryDTO dummyShowStoryDTO;
        public UpdateStory dummyUpdateStoryDTO;

        public DummyStories()
        {
            dummystory1 = A.Fake<Story>(y => y.WithArgumentsForConstructor(() => new Story()));
            dummystory1.Id = 1;
            dummystory1.AuthorID = 1;
            dummystory1.Title = "title";
            dummystory1.Description = "description";
            dummystory1.Topic = "topic";
            dummystory1.Difficulty = "difficulty";
            dummystory1.CreationTime = DateTime.Now;
            dummystory1.LastModifiedTime = DateTime.Now;
            //dummystory1.User = A.Fake<User>();

            dummystory2 = A.Fake<Story>(y => y.WithArgumentsForConstructor(() => new Story()));
            dummystory2.Id = 2;
            dummystory2.AuthorID = 2;
            dummystory2.Title = "title2";
            dummystory2.Description = "description2";
            dummystory2.Topic = "topic2";
            dummystory2.Difficulty = "difficulty2";
            dummystory2.CreationTime = DateTime.Now;
            dummystory2.LastModifiedTime = DateTime.Now;

            // creating a list
            stories = new List<Story> { dummystory1 };
            stories.Add(dummystory1);
            stories.Add(dummystory2);
            // creating a list
            storiesByUser = new List<Story> { dummystory1 };
            storiesByUser.Add(dummystory1);

            dummyShowStoryDTO = A.Fake<ShowStoryDTO>(y => y.WithArgumentsForConstructor(() => new ShowStoryDTO()));
            dummyShowStoryDTO.Id = 1;
            dummyShowStoryDTO.AuthorID = 1;
            dummyShowStoryDTO.Title = "title";
            dummyShowStoryDTO.Description = "description";
            dummyShowStoryDTO.Topic = "topic";
            dummyShowStoryDTO.Difficulty = "difficulty";
            dummyShowStoryDTO.CreationTime = DateTime.Now;
            dummyShowStoryDTO.LastModifiedTime = DateTime.Now;


            dummyUpdateStoryDTO = A.Fake<UpdateStory>(y => y.WithArgumentsForConstructor(() => new UpdateStory()));
            dummyUpdateStoryDTO.Id = 1;
            dummyUpdateStoryDTO.Title = "titleX";
            dummyUpdateStoryDTO.Description = "descriptionX";
            dummyUpdateStoryDTO.Topic = "topicX";
            dummyUpdateStoryDTO.Difficulty = "difficultyX";

            dummyStoryDTO = A.Fake<StoryDTO>(y => y.WithArgumentsForConstructor(() => new StoryDTO()));
            dummyStoryDTO.AuthorID = 1;
            dummyStoryDTO.Title = "title";
            dummyStoryDTO.Description = "description";
            dummyStoryDTO.Topic = "topic";
            dummyStoryDTO.Difficulty = "difficulty";
        }
    }
}
