using Cefalo.farhadcodes_a_CP_blog.Api.Controllers;
using Cefalo.farhadcodes_a_CP_blog.Api.UnitTests.Fixtures;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Cefalo.farhadcodes_a_CP_blog.Service.Helper;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;

namespace Cefalo.farhadcodes_a_CP_blog.Api.UnitTests.Systems
{
    public class StoryControllerUnitTests
    {

        private readonly IStoryService _storyServiceStub;
        private readonly StoryController _storyControllerStub;
        private readonly DummyStories dummyStoryObj;
        private readonly Story dummyStory, emptyStory;
        private readonly List<Story> dummyStoryList;
        private readonly List<ShowStoryDTO> dummyShowStoryDTOList;


        private readonly StoryDTO _storyDTOStub;
        private readonly ShowStoryDTO _showStoryDTOStub, emptyShowStoryStub;
        private readonly UpdateStory _updateStoryDTOStub;
        public StoryControllerUnitTests()
        {

            _storyServiceStub = A.Fake<IStoryService>();
            _storyControllerStub = new StoryController(_storyServiceStub);
            dummyStoryObj = A.Fake<DummyStories>();
            dummyStory = dummyStoryObj.dummystory1;
            dummyStoryList = dummyStoryObj.stories;

            _storyDTOStub = dummyStoryObj.dummyStoryDTO;
            _showStoryDTOStub = dummyStoryObj.dummyShowStoryDTO;
            _updateStoryDTOStub = dummyStoryObj.dummyUpdateStoryDTO;
            emptyShowStoryStub = dummyStoryObj.emptyShowStoryDTO;
            emptyStory = dummyStoryObj.dummystory3;
            dummyShowStoryDTOList = dummyStoryObj.dummyShowStoryDTOList;
        }
        #region GetStory
        [Fact]
        public async void GetStory_WithValidParameter_GetStoryIsCalledOnce()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.GetStory(dummyStory.Id)).Returns(_showStoryDTOStub);
            //Act
            var storyobj = await _storyControllerStub.GetStory(dummyStory.Id);
            //Assert
            A.CallTo(() => _storyServiceStub.GetStory(dummyStory.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetStory_WithValidParameter_GetStoryWorks()
        {
            //Arrange
            A.CallTo(() => _storyServiceStub.GetStory(dummyStory.Id)).Returns(_showStoryDTOStub);
            //Act
            var storyobj = await _storyControllerStub.GetStory(dummyStory.Id);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<OkObjectResult>();

            var newStoryObj = (OkObjectResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo(_showStoryDTOStub);
            newStoryObj.StatusCode.Should().Be(200);
        }
        [Fact]
        public async void GetStory_WithInvalidParameter_ReturnsBadRequestException()
        {
            //Arrange
            var msg = "Story with this ID NOT FOUND!";
            A.CallTo(() => _storyServiceStub.GetStory(dummyStory.Id)).Returns(emptyShowStoryStub);
            //Act
            var storyobj = await _storyControllerStub.GetStory(dummyStory.Id);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<BadRequestObjectResult>();
  
            var newStoryObj = (BadRequestObjectResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo(msg);
            newStoryObj.StatusCode.Should().Be(400);
        }
        #endregion

        #region GetPaginatedStories

        //[Fact]
        //public async void GetPaginatedStories_WithValidParameter_PaginationFilterIsCalledOnce()
        //{

        //    //Arrange
        //    var paginationFilter = new PaginationFilter(1, 4); // any

        //    A.CallTo(() => _storyServiceStub.GetPaginatedStories(paginationFilter)).Returns(dummyShowStoryDTOList);
        //    //Act
        //    var myStoryList = await _storyControllerStub.GetPaginatedStories(paginationFilter);
        //    //Assert
        //    A.CallTo(() => _storyServiceStub.GetPaginatedStories(paginationFilter)).MustHaveHappenedOnceExactly(); // withanyparameter
        //}
        //[Fact]
        //public async void GetPaginatedStories__WithValidParameter_GetPaginatedStoriesIsCalledOnce()
        //{

        //    //Arrange
        //    var paginationFilter = new PaginationFilter(1, 4);

        //    A.CallTo(() => _storyServiceStub.GetPaginatedStories(paginationFilter)).Returns(dummyShowStoryDTOList);
        //    //Act
        //    var myStoryList = await _storyControllerStub.GetPaginatedStories(paginationFilter);
        //    //Assert
        //    A.CallTo(() => _storyServiceStub.GetPaginatedStories(paginationFilter)).MustHaveHappenedOnceExactly();
        //}
        //[Fact]
        //public async void GetPaginatedStories_WithValidParameter_ReturnsStoryList()
        //{
        //    //Arrange
        //    var paginationFilter = new PaginationFilter(1, 4);

        //    A.CallTo(() => _storyServiceStub.GetPaginatedStories(paginationFilter)).Returns(dummyShowStoryDTOList);
        //    //Act
        //    var myStoryList = await _storyControllerStub.GetPaginatedStories(paginationFilter);
        //    //Assert
        //    myStoryList.Should().NotBeNull();
        //    myStoryList.Should().BeOfType<ActionResult<IEnumerable<Story>>>();
        //    myStoryList.Result.Should().BeOfType<OkObjectResult>();
        //    var myStoryListObject = (OkObjectResult)myStoryList.Result;
        //    myStoryListObject.Value.Should().BeEquivalentTo(fakePagedReponse);
        //    myStoryListObject.StatusCode.Should().Be(200);
        //}
        #endregion

        #region GetStoriesByUser
        [Fact]
        public async void GetStoriesByUser_WithValidParameter_GetStoriesByUserIsCalledOnce()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.GetStoriesByUser(dummyStory.AuthorID)).Returns(dummyShowStoryDTOList);
            //Act
            var storyobj = await _storyControllerStub.GetStoriesByUser(dummyStory.AuthorID);
            //Assert
            A.CallTo(() => _storyServiceStub.GetStoriesByUser(dummyStory.AuthorID)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetStoriesByUser_WithValidParameter_GetStoriesByUserWorks()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.GetStoriesByUser(dummyStory.AuthorID)).Returns(dummyShowStoryDTOList);
            //Act
            var storyobj = await _storyControllerStub.GetStoriesByUser(dummyStory.AuthorID);
            var storyobj2 = storyobj.Result;
            //Assert
            storyobj2.Should().NotBeNull();
            storyobj2.Should().BeOfType<OkObjectResult>();

            var temp = storyobj2 as OkObjectResult;
            temp.Value.Should().BeEquivalentTo(dummyShowStoryDTOList);
            temp.StatusCode.Should().Be(200);
        }
        #endregion

        #region PostStory

        [Fact]
        public async void PostStory_WithValidParameter_PostStoryIsCalledOnce()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.CreateStory(_storyDTOStub)).Returns(dummyStory);
            //Act
            var storyobj = await _storyControllerStub.PostStory(_storyDTOStub);
            //Assert
            A.CallTo(() => _storyServiceStub.CreateStory(_storyDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostStory_WithValidParameter_PostStoryReturnsCreatedStory()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.CreateStory(_storyDTOStub)).Returns(dummyStory);
            //Act
            var storyobj = await _storyControllerStub.PostStory(_storyDTOStub);
            //Assert
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<CreatedAtActionResult>();

            var newStoryObj = (CreatedAtActionResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo(_storyDTOStub);
            newStoryObj.StatusCode.Should().Be(201);
        }

        [Fact]
        public async void PostStory_WithInvalidParameter__ReturnsBadRequestException()
        {
            //Arrange
            var msg = "Something went wrong! Can't Create the story!";
            A.CallTo(() => _storyServiceStub.CreateStory(_storyDTOStub)).Returns(emptyStory);
            //Act
            var storyobj = await _storyControllerStub.PostStory(_storyDTOStub);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<BadRequestObjectResult>();

            var newStoryObj = (BadRequestObjectResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo(msg);
            newStoryObj.StatusCode.Should().Be(400);
        }
        #endregion

        #region UpdateStory

        [Fact]
        public async void UpdateStory_WithValidParameter_UpdateStoryIsCalledOnce()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.UpdateStory(dummyStory.Id,_updateStoryDTOStub)).Returns(dummyStory);
            //Act
            var storyobj = await _storyControllerStub.UpdateStory(dummyStory.Id, _updateStoryDTOStub);
            //Assert
            A.CallTo(() => _storyServiceStub.UpdateStory(dummyStory.Id, _updateStoryDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void UpdateStory_WithValidParameter_UpdateStoryReturnsUpdatedStory()
        {
            //Arrange
            A.CallTo(() => _storyServiceStub.UpdateStory(dummyStory.Id, _updateStoryDTOStub)).Returns(dummyStory);
            //Act
            var storyobj = await _storyControllerStub.UpdateStory(dummyStory.Id, _updateStoryDTOStub);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<OkObjectResult>();

            var newStoryObj = storyobj as OkObjectResult;
            newStoryObj.Value.Should().BeEquivalentTo(_storyDTOStub);
            newStoryObj.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void UpdateStory_WithInvalidStoryId__ReturnsBadRequestException()
        {
            //Arrange
            var msg = "Something went wrong! IDs do not match!";
            A.CallTo(() => _storyServiceStub.UpdateStory(69, _updateStoryDTOStub)).Returns(dummyStory);
            //Act
            var storyobj = await _storyControllerStub.UpdateStory(69, _updateStoryDTOStub);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<BadRequestObjectResult>();

            var newStoryObj = (BadRequestObjectResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo(msg);
            newStoryObj.StatusCode.Should().Be(400);
        }
        [Fact]
        public async void UpdateStory_WithInvalidParamter__ReturnsBadRequestException()
        {
            //Arrange
            var msg = "Something went wrong! This Story can not be found!";
            A.CallTo(() => _storyServiceStub.UpdateStory(dummyStory.Id, _updateStoryDTOStub)).Returns(emptyStory);
            //Act
            var storyobj = await _storyControllerStub.UpdateStory(dummyStory.Id, _updateStoryDTOStub);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<BadRequestObjectResult>();

            var newStoryObj = (BadRequestObjectResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo(msg);
            newStoryObj.StatusCode.Should().Be(400);
        }
        #endregion

        #region DeleteStory


        [Fact]
        public async void DeleteStory_WithValidParameter_DeleteStoryIsCalledOnce()
        {

            //Arrange
            A.CallTo(() => _storyServiceStub.DeleteStory(dummyStory.Id)).Returns(true);
            //Act
            var storyobj = await _storyControllerStub.DeleteStory(dummyStory.Id);
            //Assert
            A.CallTo(() => _storyServiceStub.DeleteStory(dummyStory.Id)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void DeleteStory_WithValidParameter_DeleteStoryReturnsNoContent()
        {
            //Arrange
            A.CallTo(() => _storyServiceStub.DeleteStory(dummyStory.Id)).Returns(true);
            //Act
            var storyobj = await _storyControllerStub.DeleteStory(dummyStory.Id);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<NoContentResult>();

            var newStoryObj = storyobj as NoContentResult;
            newStoryObj.StatusCode.Should().Be(204);
        }

        [Fact]
        public async void DeleteStory_WithInvalidStoryId__ReturnsBadRequestException()
        {
            //Arrange
            A.CallTo(() => _storyServiceStub.DeleteStory(69)).Returns(false);
            //Act
            var storyobj = await _storyControllerStub.DeleteStory(69);
            //Assert
            storyobj.Should().NotBeNull();
            storyobj.Should().BeOfType<BadRequestObjectResult>();

            var newStoryObj = (BadRequestObjectResult)storyobj;
            newStoryObj.Value.Should().BeEquivalentTo("Something went wrong! This Story can not be deleted!");
            newStoryObj.StatusCode.Should().Be(400);
        }
        #endregion
    }
}
