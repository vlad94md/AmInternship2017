using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Helpers;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using EnFlights.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EnFlights.IntegrationsTests.Infrastructure
{
    [TestClass]
    public class UserServiceIntegrationTests
    {
        private IUserService _userService;
        private IRepository<User> _userRepository;

        private User testUser;

        public UserServiceIntegrationTests()
        {
            _userRepository = new Repository<User>("DefaultConnection");
            _userService = new UserService(_userRepository);
        }

        [TestInitialize]
        public void TestInialize()
        {
            //Seed db
            var user = new User()
            {
                Email = "test@mail.com",
                Password = "12345".GetSHA256Hash(),
                DateOfBirth = new DateTime(1994, 1, 1),
                PassportExpirationDate = new DateTime(2020, 1, 1)
            };

            testUser = _userRepository.Add(user);         
        }

        [TestMethod]
        public void RegisterUser_WhenCalled_ReturnCreatedUser()
        {
            //Arrange
            var user = new User()
            {
                Email = "some@mail.com",
                Password = "44444",
                DateOfBirth = new DateTime(1994,1,1),
                PassportExpirationDate = new DateTime(2020,1,1)
            };

            //Act
            var actualResult = _userService.RegisterUser(user);

            //Assert
            Assert.AreNotEqual(Guid.Empty, actualResult.Id);
            Assert.AreNotEqual("44444", actualResult.Password);

            //Clear db
            _userRepository.Delete(actualResult);
        }

        [TestMethod]
        public void IsUsernameUnique_IsNotUnique_ReturnFalse()
        {
            //Act
            var actualResult = _userService.IsUsernameUnique("test@mail.com");

            //Assert
            Assert.AreEqual(false, actualResult);
        }

        [TestMethod]
        public void IsUsernameUnique_IsUnique_ReturnTrue()
        {
            //Act
            var actualResult = _userService.IsUsernameUnique("unique@mail.com");

            //Assert
            Assert.AreEqual(true, actualResult);
        }

        [TestMethod]
        public void IsUsernameAndPasswordCorrect_ValidDate_ReturnTrue()
        {
            //Act
            var actualResult = _userService.IsUsernameAndPasswordCorrect("test@mail.com", "12345");

            //Assert
            Assert.AreEqual(true, actualResult);
        }

        [TestMethod]
        public void IsUsernameAndPasswordCorrect_InvalidDate_ReturnFalse()
        {
            //Act
            var actualResult = _userService.IsUsernameAndPasswordCorrect("test@mail.com", "1111");

            //Assert
            Assert.AreEqual(false, actualResult);
        }

        [TestCleanup()]
        public void ClearDb()
        {
            _userRepository.Delete(testUser);
        }
    }
}
