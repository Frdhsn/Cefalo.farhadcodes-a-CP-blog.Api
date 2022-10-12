using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Repository.Repositories;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Fixtures;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Systems
{
    public class StoryServiceUnitTests
    {
        // Interfaces
        private readonly IStoryRepository _storyRepositoryStub;
        private readonly IStoryService _storyServiceStub;
        private readonly IMapper _mapperStub;
        private readonly IPassword _passwordHStub;
        private readonly IUriService _uriServiceStub;
        private readonly IHttpContextAccessor _httpContextAccessorStub;
        // DTO validators
        private readonly BaseDTOValidator<UpdateStory> _updatestorydtovalidatorStub;
        private readonly BaseDTOValidator<StoryDTO> _storydtovalidatorStub;
        private readonly BaseDTOValidator<ShowStoryDTO> _showStorydtovalidatorStub;
        // dummy stories
        private readonly Story dummystory;
        private readonly DummyStories dummystories;
        // dummy DTOS
        private readonly StoryDTO _storyDTOStub;
        private readonly ShowStoryDTO _showStoryDTOStub;
        private readonly UpdateStory _updateStoryDTOStub;
        // Constructor
        public StoryServiceUnitTests()
        {

            // Interfaces

            _storyRepositoryStub = A.Fake<IStoryRepository>();
            _mapperStub = A.Fake<IMapper>();
            _passwordHStub = A.Fake<IPassword>();
            _uriServiceStub = A.Fake<IUriService>();
            _httpContextAccessorStub = A.Fake<IHttpContextAccessor>();
            // DTO validators
            _updatestorydtovalidatorStub = A.Fake<BaseDTOValidator<UpdateStory>>();
            _showStorydtovalidatorStub = A.Fake<BaseDTOValidator<ShowStoryDTO>>();
            _storydtovalidatorStub = A.Fake<BaseDTOValidator<StoryDTO>>();
            // service instance
            _storyServiceStub = new StoryService(_httpContextAccessorStub, _uriServiceStub, _storyRepositoryStub, _mapperStub, _passwordHStub, _updatestorydtovalidatorStub, _storydtovalidatorStub);
            // dummy stories
            dummystories = A.Fake<DummyStories>();
            dummystory = dummystories.dummystory2;
            // DTOs
            _storyDTOStub = dummystories.dummyStoryDTO;
            _showStoryDTOStub = dummystories.dummyShowStoryDTO;
            _updateStoryDTOStub = dummystories.dummyUpdateStoryDTO;

        }
        #region GetStories
        [Fact]
        public async void GetStories_WithValidParameter_GetsAllStories()
        {
            //Arrange
            var listOfStories = new List<Story>();
            A.CallTo(() => _storyRepositoryStub.GetStories()).Returns(listOfStories);
            //var _storyService = new StoryService(_httpContextAccessorStub,_uriServiceStub, _storyRepositoryStub,_mapperStub,_passwordHStub,_updatestorydtovalidatorStub, _storydtovalidatorStub);
            var mappedlist =  listOfStories.Select(story => _mapperStub.Map<ShowStoryDTO>(story)).ToList();

            //Act
            var stories = await _storyServiceStub.GetStories();
            //Assert
            Assert.Equal(mappedlist,stories);
        }
        #endregion

        #region GetStory


        [Fact]
        public async void GetStory_WithValidParameter_IsCalledOnce()
        {
            // Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            // Act
            var story = await _storyServiceStub.GetStory(dummystory.Id);
            // Assert
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetStory_WithValidParameter_ValidStory()
        {
            // Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _mapperStub.Map<ShowStoryDTO>(dummystory)).Returns(_showStoryDTOStub);
            // Act
            var story = await _storyServiceStub.GetStory(dummystory.Id);
            // Assert
            story.Should().NotBeNull();
            story.Should().BeEquivalentTo(_showStoryDTOStub);
        }
        [Fact]
        public async void GetStory_WithInvalidParameter_ReturnsException()
        {

            //Arrange
            Story? nullStory = null;
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(nullStory);
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.GetStory(dummystory.Id));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be("No Story was found with that Id");
        }
        #endregion

        #region CreateStory
        [Fact]
        public async void CreateStory_WithValidParameter_ValidateDTOIsCalledOneTime()
        {
            //Arrange
            A.CallTo(() => _storydtovalidatorStub.ValidateDTO(_storyDTOStub)).DoesNothing();
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(_storyDTOStub.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_storyDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.CreateStory(dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.CreateStory(_storyDTOStub);
            //Assert
            A.CallTo(() => _storydtovalidatorStub.ValidateDTO(_storyDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostStoryAsync_WithValidParameter_MapperInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _storydtovalidatorStub.ValidateDTO(_storyDTOStub)).DoesNothing();
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(_storyDTOStub.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_storyDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.CreateStory(dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.CreateStory(_storyDTOStub);
            //Assert
            A.CallTo(() => _mapperStub.Map<Story>(_storyDTOStub)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void CreateStory_WithValidParameter_IsCalledOneTime()
        {
            //Arrange

            A.CallTo(() => _storydtovalidatorStub.ValidateDTO(_storyDTOStub)).DoesNothing();
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(_storyDTOStub.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_storyDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.CreateStory(dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.CreateStory(_storyDTOStub);
            //Assert
            A.CallTo(() => _storyRepositoryStub.CreateStory(dummystory)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void CreateStory_WithValidParameter_ReturnsCreatedStory()
        {
            //Arrange
            A.CallTo(() => _storydtovalidatorStub.ValidateDTO(_storyDTOStub)).DoesNothing();
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(_storyDTOStub.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_storyDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.CreateStory(dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.CreateStory(_storyDTOStub);
            //Assert
            myStory.Should().NotBeNull();
            myStory.Should().BeEquivalentTo(dummystory);
        }
        // two test canbe written on badreqexception and unauthorisedexception
        #endregion

        #region UpdateStory

        [Fact]
        public async void UpdateStory_WithValidParameter_ValidateDTOIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id,dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).MustHaveHappenedOnceExactly();
        }
        #endregion
    }
}
