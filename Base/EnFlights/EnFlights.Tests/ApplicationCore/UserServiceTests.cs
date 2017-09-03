using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IRepository<User>> _mockRepository;

        [TestInitialize]
        public void TestInialize()
        {
            _mockRepository = new Mock<IRepository<User>>();

            var users = new List<User>()
            {
                new User()
                {
                    Email = "test@mail.com"
                },
                new User()
                {
                    Email = "test2@mail.com"
                }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(users);

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

        //[TestMethod]
        //public void Attend_AttendanceAlreadyExists_ReturnBadRequest()
        //{
        //    var attendance = new Attendance();
        //    attendance.GigId = _gigId;
        //    attendance.AttendeeId = _userId;

        //    _mockRepository.Setup(r => r.GetAttendance(_gigId, _userId)).Returns(attendance);

        //    var result = _controller.Attend(new AttendanceDto() { GigId = _gigId });

        //    result.Should().BeOfType<BadRequestErrorMessageResult>();
        //}
    }
}
