using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Helpers;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IRepository<User>> _mockRepository;

        private User testUser;

        [TestInitialize]
        public void TestInialize()
        {
            _mockRepository = new Mock<IRepository<User>>();

            var hashedPassword = "12345".GetSHA256Hash();

                var users = new List<User>()
                {
                    new User()
                    {
                        Email = "test@mail.com",
                        Password = hashedPassword
                    },
                    new User()
                    {
                        Email = "test2@mail.com",
                        Password = hashedPassword
                    }
                };

            testUser = new User()
            {
                Email = "test@mail.com",
                Password = hashedPassword
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(users);
            _mockRepository.Setup(r => r.Add(testUser)).Returns(new User()
            {
                Email = testUser.Email,
                Password = testUser.Password.GetSHA256Hash(),
                Id = Guid.NewGuid()
            });

            _userService = new UserService(_mockRepository.Object);
        }

        [TestMethod]
        public void IsUsernamUnique_UsernameAlreadyExists_ReturnFalse()
        {
            //Arrange
            var username = "test@mail.com";

            //Act
            var actualResult =_userService.IsUsernameUnique(username);

            //Assert
            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void IsUsernamUnique_UsernameNotExists_ReturnTrue()
        {
            //Arrange
            var username = "another-test@mail.com";

            //Act
            var actualResult = _userService.IsUsernameUnique(username);

            //Assert
            Assert.AreEqual(true, actualResult);
        }

        [TestMethod]
        public void IsUsernameAndPasswordCorrect_CorrectData_ReturnTrue()
        {
            //Arrange
            var username = "test@mail.com";
            var password = "12345";

            //Act
            var actualResult = _userService.IsUsernameAndPasswordCorrect(username, password);

            //Assert
            Assert.AreEqual(true, actualResult);
        }

        [TestMethod]
        public void IsUsernameAndPasswordCorrect_WrongPass_ReturnFalse()
        {
            //Arrange
            var username = "test@mail.com";
            var password = "11111";

            //Act
            var actualResult = _userService.IsUsernameAndPasswordCorrect(username, password);

            //Assert
            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void RegisterUser_CorrectUser_ReturnSameUserWithId()
        {
            //Act
            var actualResult = _userService.RegisterUser(testUser);

            //Assert
            Assert.AreEqual(testUser.Email, actualResult.Email);
            Assert.AreEqual(testUser.Password, actualResult.Password);
            Assert.AreNotEqual(testUser.Id, actualResult.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void RegisterUser_NullUser_ReturnException()
        {
            //Act
            _userService.RegisterUser(null);
        }
    }
}
