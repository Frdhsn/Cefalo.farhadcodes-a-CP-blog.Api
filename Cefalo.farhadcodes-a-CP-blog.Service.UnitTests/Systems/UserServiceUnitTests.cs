using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Fixtures;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Systems
{
    public class UserServiceUnitTests
    {

        private readonly IMapper _mapperStub;
        private readonly IPassword _passwordH;
        private readonly IUserRepository _userRepositoryStub;
        private readonly IUserService _userServiceStub;

        private readonly BaseDTOValidator<SignUpDTO> _signupdtovalidatorStub;
        private readonly BaseDTOValidator<UserDTO> _userdtovalidatorStub;

        private readonly DummyUser dummyUserObj;
        private readonly User dummyUser;
        private readonly List<User> dummyUserList;

        private readonly UserDTO dummyUserDTO;
        private readonly List<UserDTO> dummyUserDTOList;
        public UserServiceUnitTests()
        {
            _userRepositoryStub = A.Fake<IUserRepository>();
            _mapperStub = A.Fake<IMapper>();
            _passwordH = A.Fake<IPassword>();
            _signupdtovalidatorStub = A.Fake<BaseDTOValidator<SignUpDTO>>();
            _userdtovalidatorStub = A.Fake<BaseDTOValidator<UserDTO>>();

            _userServiceStub = new UserService(_userRepositoryStub,_passwordH,_mapperStub,_userdtovalidatorStub);

            dummyUserObj = A.Fake<DummyUser>();
            dummyUser = dummyUserObj.dummyUser;
            dummyUserDTO = dummyUserObj.dummyUserDTO;
            dummyUserList = dummyUserObj.dummyUserList;
            dummyUserDTOList = dummyUserObj.dummyUserDTOList;
        }

        #region GetAllUsers
        [Fact]
        public async void GetAllUsers_WithValidParameter_GetAllUsersIsCalledOnce()
        {
            //Arrange
            var listOfUsers = new List<User>();

            A.CallTo(() => _userRepositoryStub.GetAllUsers()).Returns(listOfUsers);
            var mappedList = listOfUsers.Select(user => _mapperStub.Map<UserDTO>(user)).ToList();

            //Act
            var myUserDtos = await _userServiceStub.GetAllUsers();
            //Assert
            A.CallTo(() => _userRepositoryStub.GetAllUsers()).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetAllUsers_WithValidParameter_MapperIsCalledOnce()
        {
            //Arrange
            var listOfUsers = new List<User>();

            A.CallTo(() => _userRepositoryStub.GetAllUsers()).Returns(listOfUsers);
            var mappedList = listOfUsers.Select(user => _mapperStub.Map<UserDTO>(user)).ToList();
            //Act
            var allusers = await _userServiceStub.GetAllUsers();
            //Assert
            Assert.Equal(mappedList, allusers);
        }
        [Fact]
        public async void GetAllUsers_WithValidParameter_ReturnsUserDTOList()
        {
            //Arrange
            var listOfUsers = new List<User>();

            A.CallTo(() => _userRepositoryStub.GetAllUsers()).Returns(listOfUsers);
            var mappedList = listOfUsers.Select(user => _mapperStub.Map<UserDTO>(user)).ToList();
            //Act
            var myUserDtos = await _userServiceStub.GetAllUsers();
            //Assert
            myUserDtos.Should().NotBeNull();
            myUserDtos.Should().BeEquivalentTo(listOfUsers);
            myUserDtos.Should().BeOfType<List<UserDTO>>();
        }
        #endregion

        #region GetUser
        [Fact]
        public async void GetUser_WithValidParameter_GetUserIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var user = await _userServiceStub.GetUser(dummyUser.Id);
            //Assert
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetUser_WithValidParameter_MapperIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var user = await _userServiceStub.GetUser(dummyUser.Id);
            //Assert
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetUser_WithValidParameter_ReturnsValidUser()
        {
            //Arrange
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var user = await _userServiceStub.GetUser(dummyUser.Id);
            //Assert
            user.Should().NotBeNull();
            user.Should().BeEquivalentTo(dummyUserDTO);
        }
        [Fact]
        public async void GetUser_WithInvalidParameter_ReturnsNotFoundException()
        {
            //Arrange
            User? tmp = null;
            var msg = "No user was found with that ID!";
            A.CallTo(() => _userRepositoryStub.GetUser(69)).Throws(new NotFoundHandler(msg));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _userServiceStub.GetUser(69));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(msg);
            exception.GetType().Should().Be(typeof(NotFoundHandler));
        }
        #endregion

        #region GetUserByEmail
        [Fact]
        public async void GetUserByEmail_WithValidParameter_GetUserIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var user = await _userServiceStub.GetUser(dummyUser.Id);
            //Assert
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetUserByEmail_WithValidParameter_MapperIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var user = await _userServiceStub.GetUser(dummyUser.Id);
            //Assert
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void GetUserByEmail_WithValidParameter_ReturnsValidUser()
        {
            //Arrange
            A.CallTo(() => _userRepositoryStub.GetUser(dummyUser.Id)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var user = await _userServiceStub.GetUser(dummyUser.Id);
            //Assert
            user.Should().NotBeNull();
            user.Should().BeEquivalentTo(dummyUserDTO);
        }
        [Fact]
        public async void GetUserByEmail_WithInvalidParameter_ReturnsNotFoundException()
        {
            //Arrange
            User? tmp = null;
            var msg = "No user was found with that ID!";
            A.CallTo(() => _userRepositoryStub.GetUser(69)).Throws(new NotFoundHandler(msg));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _userServiceStub.GetUser(69));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(msg);
            exception.GetType().Should().Be(typeof(NotFoundHandler));
        }
        #endregion

        #region PostUser
        [Fact]
        public async void PostUser_WithValidParameter_ValidateDTOIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).DoesNothing();
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).Returns(dummyUser);
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var createdUser = await _userServiceStub.PostUser(dummyUserDTO);
            //Assert
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostUser_WithValidParameter_MapperIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).DoesNothing();
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).Returns(dummyUser);
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var createdUser = await _userServiceStub.PostUser(dummyUserDTO);
            //Assert
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostUser_WithValidParameter_PostUserIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).DoesNothing();
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).Returns(dummyUser);
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var createdUser = await _userServiceStub.PostUser(dummyUserDTO);
            //Assert
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostUser_WithValidParameter_Mapper2IsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).DoesNothing();
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).Returns(dummyUser);
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var createdUser = await _userServiceStub.PostUser(dummyUserDTO);
            //Assert
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void PostUser_WithValidParameter_ReturnsUserCorrectly()
        {
            //Arrange
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).DoesNothing();
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).Returns(dummyUser);
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            //Act
            var createdUser = await _userServiceStub.PostUser(dummyUserDTO);
            //Assert
            createdUser.Should().NotBeNull();
            createdUser.Should().BeEquivalentTo(dummyUserDTO);
        }
        [Fact]
        public async void PostUser_WithInvalidParameter_ReturnsBadRequestException()
        {
            //Arrange
            A.CallTo(() => _mapperStub.Map<User>(dummyUserDTO)).Returns(dummyUser);
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            var msg = "Invalid information!";
            A.CallTo(() => _userdtovalidatorStub.ValidateDTO(dummyUserDTO)).Throws(new BadRequestHandler(msg));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _userServiceStub.PostUser(dummyUserDTO));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(msg);
        }
        #endregion

        #region UpdateUser
        #endregion

        #region DeleteUser
        #endregion
    }
}
