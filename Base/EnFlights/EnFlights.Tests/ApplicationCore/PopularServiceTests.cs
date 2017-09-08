using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class PopularServiceTests
    {
        private IPopularService _popularService;
        private Mock<IRepository<TicketOrder>> _mockOrderRepository;

        private List<City> cities;
        private List<Flight> flights;
        private List<TicketOrder> ticketOrders;

        [TestInitialize]
        public void TestInialize()
        {
            _mockOrderRepository = new Mock<IRepository<TicketOrder>>();

            cities = new List<City>
            {
                new City() {Name = "London"},
                new City() {Name = "Moscow"},
                new City() {Name = "Kiev"},
                new City() {Name = "Berlin"},
                new City() {Name = "Paris"},
                new City() {Name = "Dublin"},
                new City() {Name = "Rome"},
            };

            flights = new List<Flight>
            {
                new Flight() {Name = "L1", CityTo = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "M1", CityTo = cities.Find(x => x.Name == "Moscow")},
                new Flight() {Name = "K1", CityTo = cities.Find(x => x.Name == "Kiev")},
                new Flight() {Name = "B1", CityTo = cities.Find(x => x.Name == "Berlin")},
                new Flight() {Name = "P1", CityTo = cities.Find(x => x.Name == "Paris")},
                new Flight() {Name = "D1", CityTo = cities.Find(x => x.Name == "Dublin")},
                new Flight() {Name = "L2", CityTo = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "R1", CityTo = cities.Find(x => x.Name == "Rome")},
            };

            ticketOrders = new List<TicketOrder>()
            {
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L1")}, //London 5
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L1")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L1")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L2")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L2")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "K1")}, //Kiev 2
                new TicketOrder() {Flight = flights.Find(x => x.Name == "K1")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "M1")}, //Moscow 3
                new TicketOrder() {Flight = flights.Find(x => x.Name == "M1")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "M1")},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "P1")}, //Paris 1
                new TicketOrder() {Flight = flights.Find(x => x.Name == "D1")}, //Dublin 1
                new TicketOrder() {Flight = flights.Find(x => x.Name == "R1")}, //Rome 1
            };
        }

        [TestMethod]
        public void GetTop5PopularDestinations_NoOrdersExist_ReturnFirst5Destinations()
        {
            //Arrange
            _mockOrderRepository.Setup(x => x.GetAll()).Returns(new List<TicketOrder>());
            _popularService = new PopularService(_mockOrderRepository.Object);

            //Act
            var actualResult = _popularService.GetTop5PopularDestinations();

            //Assert
            Assert.AreEqual(0, actualResult.Count);
        }

        [TestMethod]
        public void GetTop5PopularDestinations_OrdersExist_ReturnFirst5Destinations()
        {
            //Arrange
            _mockOrderRepository.Setup(x => x.GetAll()).Returns(ticketOrders);
            _popularService = new PopularService(_mockOrderRepository.Object);

            //Act
            var actualResult = _popularService.GetTop5PopularDestinations();

            //Assert
            Assert.AreEqual(5, actualResult.Count);
            Assert.AreEqual("London", actualResult[0].Name);
            Assert.AreEqual("Moscow", actualResult[1].Name);
            Assert.AreEqual("Kiev", actualResult[2].Name);
        }
    }
}
