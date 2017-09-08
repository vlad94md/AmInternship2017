using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class TicketOrderServiceTests
    {
        private ITicketOrderService _ticketService;
        private Mock<IRepository<TicketOrder>> _mockOrderRepository;

        [TestInitialize]
        public void TestInialize()
        {
            _mockOrderRepository = new Mock<IRepository<TicketOrder>>();

            var orders = new List<TicketOrder>()
            {
                new TicketOrder() {Owner = new User() {Email = "test@mail.com"}, Flight = new Flight() {Name = "M1"} },
                new TicketOrder() {Owner = new User() {Email = "test@mail.com"}, Flight = new Flight() {Name = "L1"} },
                new TicketOrder() {Owner = new User() {Email = "other@mail.com"}, Flight = new Flight() {Name = "T1"} }
            };

            _mockOrderRepository.Setup(x => x.GetAll()).Returns(orders);
            _ticketService = new TicketOrderService(_mockOrderRepository.Object);
        }


        [TestMethod]
        public void GetUserBookings_UserMadeTwoOrders_ReturnBookings()
        {
            //Arrange
            var username = "test@mail.com";

            //Act
            var actualResult = _ticketService.GetUserBookings(username);

            //Assert
            Assert.AreEqual(2, actualResult.Count);
            Assert.AreEqual(true, actualResult.Exists(x => x.Flight.Name == "M1"));
            Assert.AreEqual(true, actualResult.Exists(x => x.Flight.Name == "L1"));
        }

        [TestMethod]
        public void GetUserBookings_UserMadeNoOrders_ReturnEmptyList()
        {
            //Arrange
            var username = "fake@mail.com";

            //Act
            var actualResult = _ticketService.GetUserBookings(username);

            //Assert
            Assert.AreEqual(0, actualResult.Count);
        }

        [TestMethod]
        public void GetUserBookings_InvalidUsername_ReturnEmptyList()
        {
            //Act
            var actualResult = _ticketService.GetUserBookings(null);

            //Assert
            Assert.AreEqual(0, actualResult.Count);
        }

    }
}
