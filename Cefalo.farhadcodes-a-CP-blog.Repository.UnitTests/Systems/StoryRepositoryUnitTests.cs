using Cefalo.farhadcodes_a_CP_blog.Database.Context;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Repository.UnitTests.Systems
{
    public class StoryRepositoryUnitTests
    {
        private readonly CPContext _cpContext;
        public StoryRepositoryUnitTests()
        {
            var options = new DbContextOptionsBuilder<CPContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _cpContext = new CPContext(options);
            _cpContext.Database.EnsureCreated();
        }
        private async void AddStories(int n)
        {
            if (await _cpContext.Stories.CountAsync() <= 0)
            {
                for (int i = 1; i <= n; i++)
                {
                    _cpContext.Stories.Add(new Story()
                    {
                        Id = i,
                        AuthorID = i,
                        Title = $"title{i}",
                        Topic = $"topic{i}",
                        Description = $"description{i}",
                        Difficulty = $"difficulty{i}",
                        CreationTime = DateTime.UtcNow,
                        LastModifiedTime = DateTime.UtcNow,
                    });
                    await _cpContext.SaveChangesAsync();
                }
            }
        }
        private async void AddStory(Story body)
        {
            _cpContext.Stories.Add(body);
            await _cpContext.SaveChangesAsync();
        }

        #region GetStories
        [Fact]
        public async void GetStories_WithValidParameters_GetsAllStories()
        {
            // Arrange
            AddStories(10);
            var storyRepository = new StoryRepository(_cpContext);
            // Act
            var res = await storyRepository.GetStories();
            // Assert
            Assert.Equal(res, _cpContext.Stories);
            Assert.NotNull(res);
        }
        #endregion

        #region GetPaginatedStories

        //[Fact]
        //public async void GetPaginatedStories_PageSizeIsThree_ReturnsThreeAuthors()
        //{
        //}
        #endregion

        #region GetStory

        [Fact]
        public async void GetStory_WithValidParameters_ReturnsCorrectStory()
        {

            // Arrange
            AddStories(2);
            var storyRepository = new StoryRepository(_cpContext);
            // Act
            var res = await storyRepository.GetStory(1);
            // Assert

            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
        }
        #endregion

        #region CreateStory

        [Fact]
        public async void CreateStory_WithValidParameters_CreatesStory()
        {
            // Arrange
            Story dummyStory = new Story()
            {

                Id = 69,
                AuthorID = 69,
                Title = $"title69",
                Topic = $"topic69",
                Description = $"description69",
                Difficulty = $"difficulty69",
                CreationTime = DateTime.UtcNow,
                LastModifiedTime = DateTime.UtcNow,
            };
            var storyRepository = new StoryRepository(_cpContext);
            // Act
            var res = await storyRepository.CreateStory(dummyStory);
            // Assert
            Assert.Equal(res, dummyStory);
            Assert.NotNull(res);
        }
        #endregion

        #region UpdateStory

        [Fact]
        public async void UpdateStory_WithValidParameters_UpdatesStory()
        {
            // Arrange
            Story dummyStory1 = new Story()
            {

                Id = 69,
                AuthorID = 69,
                Title = "title69",
                Topic = "topic69",
                Description = "description69",
                Difficulty = "difficulty69",
                CreationTime = DateTime.UtcNow,
                LastModifiedTime = DateTime.UtcNow,
            };
            AddStory(dummyStory1);
            Story dummyStory2 = new Story()
            {

                Id = 69,
                Title = "title70",
                Topic = "topic69",
                Description = "description69",
                Difficulty = "difficulty69"
            };
            var storyRepository = new StoryRepository(_cpContext);
            // Act
            var res = await storyRepository.UpdateStory(dummyStory1.Id,dummyStory2);
            // Assert
            Assert.Equal(res.Title, "title70");
            Assert.NotNull(res);
        }
        #endregion
    }
}
