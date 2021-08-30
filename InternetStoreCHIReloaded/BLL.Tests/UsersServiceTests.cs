using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests
{
    public class UsersServiceTests
    {
        private readonly IUsersService _usersService;

        public UsersServiceTests(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Fact]
        public void UserRegistrationSucceeds()
        {
            var expectedResponse = new DbResponse
            {
                IsSuccessful = true,
                Message = "User registration success!"
            };
            var userRegistrationModel = new UserRegistrationModel
            {
                Username = "Ayrum1158",
                Password = "SaSha-K1158",
                ConfirmPassword = "SaSha-K1158",
                FirstName = "Ole",
                LastName = "Koro"
            };
            var expected = expectedResponse;
            var actual = _usersService.RegisterUserAsync(userRegistrationModel).Result;

            Assert.True(actual != null);
            Assert.Equal(actual.IsSuccessful, expected.IsSuccessful);
            Assert.Equal(actual.Message, expected.Message);
        }

        [Fact]
        public void UserNicknameDuplicationError()
        {
            var expectedResponse = new DbResponse
            {
                IsSuccessful = false
            };
            var userRegistrationModel = new UserRegistrationModel
            {
                Username = "Ayrum1158",
                Password = "SaSha-K1158",
                ConfirmPassword = "SaSha-K1158",
                FirstName = "Ole",
                LastName = "Koro"
            };
            var expected = expectedResponse;

            _usersService.RegisterUserAsync(userRegistrationModel);
            var actual = _usersService.RegisterUserAsync(userRegistrationModel).Result;

            string expectedErrorMessage = "Errors occured while creating user";
            string actualErrorMessage = actual.Message.Split(':')[0];

            Assert.True(actual != null);
            Assert.Equal(actual.IsSuccessful, expected.IsSuccessful);
            Assert.Equal(actualErrorMessage, expectedErrorMessage);
        }

        [Fact]
        public void LoginAtemptSucceded()
        {
            var userRegistrationModel = new UserRegistrationModel
            {
                Username = "Ayrum1158",
                Password = "SaSha-K1158",
                ConfirmPassword = "SaSha-K1158",
                FirstName = "Ole",
                LastName = "Koro"
            };
            _usersService.RegisterUserAsync(userRegistrationModel);

            var userLoggingInModel = new UserLoggingInModel
            {
                Username = "Ayrum1158",
                Password = "SaSha-K1158"
            };
            var expected = new ServiceResult<string>
            {
                IsSuccessful = true,
                Message = "User logged in successfully!"
            };
            var actual = _usersService.LoginUserAsync(userLoggingInModel).Result;

            Assert.True(actual != null);
            Assert.Equal(actual.IsSuccessful, expected.IsSuccessful);
            Assert.Equal(actual.Message, expected.Message);
            Assert.False(string.IsNullOrEmpty(actual.Data));
        }

        [Fact]
        public void LoginAttemptFailsWithWrongLoginData()
        {
            var userRegistrationModel = new UserRegistrationModel
            {
                Username = "Ayrum1158",
                Password = "SaSha-K1158",
                ConfirmPassword = "SaSha-K1158",
                FirstName = "Ole",
                LastName = "Koro"
            };
            _usersService.RegisterUserAsync(userRegistrationModel);

            var userLoggingInModel = new UserLoggingInModel
            {
                Username = "NonExistingUser",
                Password = "NonExistingPassword"
            };
            var expected = new ServiceResult<string>
            {
                IsSuccessful = false,
                Message = "Check your login data."
            };
            var actual = _usersService.LoginUserAsync(userLoggingInModel).Result;

            Assert.True(actual != null);
            Assert.Equal(actual.IsSuccessful, expected.IsSuccessful);
            Assert.Equal(actual.Message, expected.Message);
            Assert.Null(actual.Data);
        }
    }
}
