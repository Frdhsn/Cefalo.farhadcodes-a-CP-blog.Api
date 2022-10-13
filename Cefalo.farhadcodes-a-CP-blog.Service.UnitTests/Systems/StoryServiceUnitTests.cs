using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Repository.Repositories;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Fixtures;
using Cefalo.farhadcodes_a_CP_blog.Service.Wrappers;
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
        private readonly List<Story> storiesByUser1;
        // dummy DTOS
        private readonly StoryDTO _storyDTOStub;
        private readonly ShowStoryDTO _showStoryDTOStub;
        private readonly UpdateStory _updateStoryDTOStub;
        // pagination filter
        private readonly PaginationFilter dummypgfilter;
        #region Constructor
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
            dummystory = dummystories.dummystory1;
            storiesByUser1 = dummystories.storiesByUser;
            
            // DTOs
            _storyDTOStub = dummystories.dummyStoryDTO;
            _showStoryDTOStub = dummystories.dummyShowStoryDTO;
            _updateStoryDTOStub = dummystories.dummyUpdateStoryDTO;
            // pagination filter
            dummypgfilter = new PaginationFilter(1, 4);
        }
        #endregion

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

        #region GetPaginatedStories
        [Fact]
        public async void GetPaginatedStories_WithValidParameter_GetPaginatedStoriesIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetPaginatedStories(dummypgfilter.PageNumber,dummypgfilter.PageSize)).Returns(storiesByUser1);
            //Act
            var myStoryList = await _storyServiceStub.GetPaginatedStories(dummypgfilter);
            //Assert
            A.CallTo(() => _storyRepositoryStub.GetPaginatedStories(dummypgfilter.PageNumber, dummypgfilter.PageSize)).MustHaveHappenedOnceExactly();
        }
        //[Fact]
        //public async void GetPaginatedStories_WithValidParameter_ReturnedStoryListIsValid()
        //{
        //    //Arrange

        //    var listOfStories = new List<Story>();
        //    A.CallTo(() => _storyRepositoryStub.GetPaginatedStories(dummypgfilter.PageNumber, dummypgfilter.PageSize)).Returns(listOfStories);
        //    var mappedlist = listOfStories.Select(story => _mapperStub.Map<ShowStoryDTO>(story)).ToList();
        //    //Act
        //    var myStoryList = await _storyServiceStub.GetPaginatedStories(dummypgfilter);
        //    //Assert
        //    Assert.Equal(mappedlist, myStoryList);
        //}
        #endregion
        #region GetStoriesByUser
        [Fact]
        public async void GetStoriesByUser_WithValidParameter_GetStoryByUserIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetStoriesByUser(dummystory.AuthorID)).Returns(storiesByUser1);
            A.CallTo(() => _mapperStub.Map<ShowStoryDTO>(dummystory)).Returns(_showStoryDTOStub);
            //Act
            var myStoryList = await _storyServiceStub.GetStoriesByUser(dummystory.AuthorID);
            //Assert
            A.CallTo(() => _storyRepositoryStub.GetStoriesByUser(dummystory.AuthorID)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetStoriesByUser_WithValidParameter_ReturnedStoryListIsValid()
        {
            //Arrange
            var listOfStories = new List<Story>();
            A.CallTo(() => _storyRepositoryStub.GetStoriesByUser(dummystory.AuthorID)).Returns(listOfStories);
            var mappedlist = listOfStories.Select(story => _mapperStub.Map<ShowStoryDTO>(story)).ToList();
            //Act
            var myStoryList = await _storyServiceStub.GetStoriesByUser(dummystory.AuthorID);
            //Assert
            Assert.Equal(mappedlist, myStoryList);
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
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id,dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void UpdateStory_WithValidParameter_GetLoggedInIdIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id, dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _passwordHStub.GetLoggedInId()).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void UpdateStory_WithValidParameter_GetStoryIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id, dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void UpdateStory_WithValidParameter_UpdateStoryIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id, dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id, dummystory)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void UpdateStory_WithValidParameter_MapperIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id, dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void UpdateStory_WithValidParameter_ReturnsUpdatedStoryCorrectly()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            A.CallTo(() => _mapperStub.Map<Story>(_updateStoryDTOStub)).Returns(dummystory);
            A.CallTo(() => _storyRepositoryStub.UpdateStory(dummystory.Id, dummystory)).Returns(dummystory);
            //Act
            var myStory = await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub);
            //Assert
            myStory.Should().NotBeNull();
            myStory.Should().BeEquivalentTo(dummystory);

        }
        [Fact]
        public async void UpdateStory_WithInvalidStoryId_ReturnsBadRequestException()
        {
            //Arrange
            var errMessage = "Bad request! Story Id doesn't match!";
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).Throws(new BadRequestHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);

        }
        [Fact]
        public async void UpdateStory_WithInvalidStoryId_ReturnsNotFoundException()
        {
            //Arrange
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            var errMessage = "No Story was found with that Id";
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Throws(new BadRequestHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);

        }
        [Fact]
        public async void UpdateStory_AuthorIdDoesNotMatchLoggedInId_ReturnsForbiddenException()
        {

            //Arrange
            var errMessage = "You don't have the permission!";
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            int wrongId = 69;
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(wrongId);
            //Act
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);
            //exception.GetType().Should().Be(typeof(ForbiddenHandler));
        }
        [Fact]
        public async void UpdateStory_WithNoTokenExists_ReturnsUnAuthorisedException()
        {
            //Arrange
            var errMessage = "You're not logged in! Please log in to get access.";
            A.CallTo(() => _updatestorydtovalidatorStub.ValidateDTO(_updateStoryDTOStub)).DoesNothing();
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            //var ctime = String.Empty;
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Throws(new UnauthorisedHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.UpdateStory(dummystory.Id, _updateStoryDTOStub));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);
            exception.GetType().Should().Be(typeof(UnauthorisedHandler));
        }
        #endregion

        #region DeleteStory
        [Fact]
        public async void DeleteStory_WithValidParameter_GetStoryIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            var ctime = "ctime";
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Returns(ctime);
            A.CallTo(() => _storyRepositoryStub.DeleteStory(dummystory.Id)).Returns(true);
            //Act
            var myStory = await _storyServiceStub.DeleteStory(dummystory.Id);
            //Assert
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void DeleteStory_WithValidParameter_GetLoggedInIdIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            var ctime = "ctime";
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Returns(ctime);
            A.CallTo(() => _storyRepositoryStub.DeleteStory(dummystory.Id)).Returns(true);
            //Act
            var myStory = await _storyServiceStub.DeleteStory(dummystory.Id);
            //Assert
            A.CallTo(() => _passwordHStub.GetLoggedInId()).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void DeleteStory_WithValidParameter_GetTokenCreationIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            var ctime = "ctime";
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Returns(ctime);
            A.CallTo(() => _storyRepositoryStub.DeleteStory(dummystory.Id)).Returns(true);
            //Act
            var myStory = await _storyServiceStub.DeleteStory(dummystory.Id);
            //Assert
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void DeleteStory_WithValidParameter_DeleteStoryIsInvokedOneTime()
        {

            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            var ctime = "ctime";
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Returns(ctime);
            A.CallTo(() => _storyRepositoryStub.DeleteStory(dummystory.Id)).Returns(true);
            //Act
            var myStory = await _storyServiceStub.DeleteStory(dummystory.Id);
            //Assert
            A.CallTo(() => _storyRepositoryStub.DeleteStory(dummystory.Id)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void DeleteStory_WithValidParameter_ReturnsTrueAfterDeletingStory()
        {

            //Arrange
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            var ctime = "ctime";
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Returns(ctime);
            A.CallTo(() => _storyRepositoryStub.DeleteStory(dummystory.Id)).Returns(true);
            //Act
            var myStory = await _storyServiceStub.DeleteStory(dummystory.Id);
            //Assert
            myStory.Should().BeTrue();
        }
        [Fact]
        public async void DeleteStory_WithInvalidStoryId_ReturnsNotFoundException()
        {
            //Arrange
            var errMessage = "No Story was found with that Id";
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Throws(new BadRequestHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.DeleteStory(dummystory.Id));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);
            exception.GetType().Should().Be(typeof(BadRequestHandler));

        }
        [Fact]
        public async void DeleteStory_AuthorIdDoesNotMatchLoggedInId_ReturnsForbiddenException()
        {
            //Arrange
            var errMessage = "You don't have the permission!";
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Throws(new ForbiddenHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.DeleteStory(dummystory.Id));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);
            exception.GetType().Should().Be(typeof(ForbiddenHandler));
        }
        [Fact]
        public async void DeleteStory_WithNoTokenExists_ReturnsUnAuthorisedException()
        {
            //Arrange
            var errMessage = "You're not logged in! Please log in to get access.";
            A.CallTo(() => _storyRepositoryStub.GetStory(dummystory.Id)).Returns(dummystory);
            A.CallTo(() => _passwordHStub.GetLoggedInId()).Returns(dummystory.AuthorID);
            //var ctime = String.Empty;
            A.CallTo(() => _passwordHStub.GetTokenCreationTime()).Throws(new UnauthorisedHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _storyServiceStub.DeleteStory(dummystory.Id));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);
            exception.GetType().Should().Be(typeof(UnauthorisedHandler));
        }
        #endregion
    }
}
