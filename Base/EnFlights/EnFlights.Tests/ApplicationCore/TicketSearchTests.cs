using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class TicketSearchTests
    {
        private ITicketSearchService _ticketService;
        private Mock<IRepository<Flight>> _mockRepository;

        private User testUser;

        [TestInitialize]
        public void TestInialize()
        {
            
        }


        [TestMethod]
        public void IsUsernamUnique_UsernameAlreadyExists_ReturnFalse()
        {
            //Arrange
            var username = "test@mail.com";

            //Act
            var actualResult = _ticketService.FindFlights(new TicketSearchModel());

            //Assert
            Assert.AreEqual(false, actualResult);
        }

    }
}
