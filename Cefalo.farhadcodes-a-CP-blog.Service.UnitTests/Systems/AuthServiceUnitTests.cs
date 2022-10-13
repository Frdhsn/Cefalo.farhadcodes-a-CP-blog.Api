using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Repository.Repositories;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Fixtures;
using Cefalo.farhadcodes_a_CP_blog.Service.Wrappers;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.UnitTests.Systems
{
    public class AuthServiceUnitTests
    {

        private readonly IMapper _mapperStub;
        private readonly IConfiguration _configStub;
        private readonly IPassword _passwordH;
        private readonly IUserRepository _userRepositoryStub;
        private readonly IAuthService _authServiceStub;
        private readonly BaseDTOValidator<LoginDTO> _logindtovalidatorStub;
        private readonly BaseDTOValidator<SignUpDTO> _signupdtovalidatorStub;
        // DTO
        private readonly LoginDTO _loginDTOStub;
        private readonly SignUpDTO _signUpDTOStub;
        private readonly UserDTO dummyUserDTO;
        // dummy user
        private readonly DummyUser dummyUserObj;
        private readonly User dummyUser;
        #region Constructor
        public AuthServiceUnitTests()
        {

            _userRepositoryStub = A.Fake<IUserRepository>();
            _mapperStub = A.Fake<IMapper>();
            _passwordH = A.Fake<IPassword>();
            _logindtovalidatorStub = A.Fake<BaseDTOValidator<LoginDTO>>();
            _signupdtovalidatorStub = A.Fake<BaseDTOValidator<SignUpDTO>>();

            _authServiceStub = new AuthService(_signupdtovalidatorStub, _logindtovalidatorStub, _userRepositoryStub, _configStub, _passwordH, _mapperStub);
            // dummy data

            dummyUserObj = A.Fake<DummyUser>();
            dummyUser = dummyUserObj.dummyUser;
            dummyUserDTO = dummyUserObj.dummyUserDTO;
            _loginDTOStub = dummyUserObj.dummyLoginDTO;
            _signUpDTOStub = dummyUserObj.dummySignUpDTO;
        }
        #endregion

        #region Login
        [Fact]
        public async void Login_WithValidParameter_ValidateDTOIsInvokedOneTime()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var myUsername = await _authServiceStub.Login(_loginDTOStub);
            //Assert
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Login_WithValidParameter_GetUserByEmailIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);
            //Act
            var myUsername = await _authServiceStub.Login(_loginDTOStub);
            //Assert
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void LoginAsync_WithValidParameter_VerifyHashIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);
            //Act
            var myUsername = await _authServiceStub.Login(_loginDTOStub);
            //Assert
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async void Login_WithValidParameter_MapperIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);
            //Act
            var myUsername = await _authServiceStub.Login(_loginDTOStub);
            //Assert
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Login_WithValidParameter_CreateTokenIsCalledOnce()
        {

            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);
            //Act
            var myUsername = await _authServiceStub.Login(_loginDTOStub);
            //Assert
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Login_WithInvalidParameter_ReturnsBadRequestExceptionFromDTOValidator()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            var errMessage = "Invalid email or password";
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).Throws(new BadRequestHandler(errMessage));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _authServiceStub.Login(_loginDTOStub));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(errMessage);
            exception.GetType().Should().Be(typeof(BadRequestHandler));
        }
        [Fact]
        public async void Login_WithInvalidEmail_ReturnsUnauthorisedException()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);
            User? noUser = null;
            var msg = "Incorrect email or password!";
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(noUser);
            //Act
            var exception = await Record.ExceptionAsync(async () => await _authServiceStub.Login(_loginDTOStub));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(msg);
            exception.GetType().Should().Be(typeof(UnauthorisedHandler));
        }
        [Fact]
        public async void Login_WithInvalidPassword_ReturnsUnauthorisedException()
        {
            //Arrange
            A.CallTo(() => _logindtovalidatorStub.ValidateDTO(_loginDTOStub)).DoesNothing();
            A.CallTo(() => _userRepositoryStub.GetUserByEmail(_loginDTOStub.Email)).Returns(dummyUser);
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(true);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            var msg = "Incorrect email or password!";
            A.CallTo(() => _passwordH.VerifyHash(_loginDTOStub.Password, A<byte[]>.That.IsInstanceOf(typeof(byte[])), A<byte[]>.That.IsInstanceOf(typeof(byte[])))).Returns(false);
            //Act
            var exception = await Record.ExceptionAsync(async () => await _authServiceStub.Login(_loginDTOStub));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(msg);
            exception.GetType().Should().Be(typeof(UnauthorisedHandler));
        }
        #endregion

        #region Signup

        [Fact]
        public async void Signup_WithValidParameter_ValidateDTOIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var myUserWithToken = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Signup_WithValidParameter_HashPasswordIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);

            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);
            //Act
            var myUserWithToken = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            A.CallTo(() => _passwordH.HashPassword(_signUpDTOStub.Password)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Signup_WithValidParameter_MapperIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var myUserWithToken = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Signup_WithValidParameter_SignupIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var myUserWithToken = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Signup_WithValidParameter_Mapper2IsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var myUserWithToken = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Signup_WithValidParameter_CreateTokenIsCalledOnce()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var myUserWithToken = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async void Signup_WithValidParameter_ReturnsSignedUpUser()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            //Act
            var createdUser = await _authServiceStub.SignUp(_signUpDTOStub);
            //Assert
            createdUser.Should().NotBeNull();
            createdUser.Should().BeEquivalentTo(dummyUserDTO);
        }
        [Fact]
        public async void Signup_WithInvalidParameter_ReturnsBadRequestException()
        {
            //Arrange
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).DoesNothing();
            A.CallTo(() => _passwordH.HashPassword(_loginDTOStub.Password)).Returns(new Tuple<byte[], byte[]>(dummyUser.PasswordSalt, dummyUser.PasswordHash));
            A.CallTo(() => _mapperStub.Map<User>(_signUpDTOStub)).Returns(dummyUser);

            A.CallTo(() => _userRepositoryStub.PostUser(dummyUser)).Returns(dummyUser);
            A.CallTo(() => _mapperStub.Map<UserDTO>(dummyUser)).Returns(dummyUserDTO);
            A.CallTo(() => _passwordH.CreateToken(dummyUser)).Returns(dummyUserDTO.Token);

            var msg = "Invalid information!";
            A.CallTo(() => _signupdtovalidatorStub.ValidateDTO(_signUpDTOStub)).Throws(new BadRequestHandler(msg));
            //Act
            var exception = await Record.ExceptionAsync(async () => await _authServiceStub.SignUp(_signUpDTOStub));
            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(msg);
        }
        #endregion
    }
}
