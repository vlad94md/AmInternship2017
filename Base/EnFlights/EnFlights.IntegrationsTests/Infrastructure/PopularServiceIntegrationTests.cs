using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using EnFlights.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EnFlights.IntegrationsTests.Infrastructure
{
    [TestClass]
    public class PopularServiceIntegrationTests
    {
        private IPopularService _popularService;
        private IRepository<TicketOrder> _orderRepository;
        private IRepository<City> _cityRepository;
        private IRepository<Flight> _flighRepository;
        private IRepository<User> _userRepository;
        private IRepository<BillingDetails> _billingRepository;

        private User testUser;

        public PopularServiceIntegrationTests()
        {
            _orderRepository = new Repository<TicketOrder>("DefaultConnection");
            _cityRepository = new Repository<City>("DefaultConnection");
            _flighRepository = new Repository<Flight>("DefaultConnection");
            _userRepository = new Repository<User>("DefaultConnection");
            _billingRepository = new Repository<BillingDetails>("DefaultConnection");
            _popularService = new PopularService(_orderRepository);
        }

        [TestInitialize]
        public void TestInialize()
        {
            var now = DateTime.Now;

            var user = new User()
            {
                Email = "unregistered_user",
                Password = "12345",
                DateOfBirth = new DateTime(1994, 1, 1),
                PassportExpirationDate = new DateTime(2020, 1, 1)
            };

            testUser = _userRepository.Add(user);

            var cities = new List<City>
            {
                new City() {Name = "London"},
                new City() {Name = "Moscow"},
                new City() {Name = "Kiev"},
                new City() {Name = "Berlin"},
                new City() {Name = "Paris"},
                new City() {Name = "Dublin"},
                new City() {Name = "Rome"},
            };

            cities.ForEach(x => _cityRepository.Add(x));

            cities = _cityRepository.GetAll();
            
            var flights = new List<Flight>
            {
                new Flight() {Name = "L1", CityTo = cities.Find(x => x.Name == "London"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "M1", CityTo = cities.Find(x => x.Name == "Moscow"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "K1", CityTo = cities.Find(x => x.Name == "Kiev"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "B1", CityTo = cities.Find(x => x.Name == "Berlin"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "P1", CityTo = cities.Find(x => x.Name == "Paris"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "D1", CityTo = cities.Find(x => x.Name == "Dublin"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "L2", CityTo = cities.Find(x => x.Name == "London"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")},
                new Flight() {Name = "R1", CityTo = cities.Find(x => x.Name == "Rome"), DepartureDate = now, ArrivalDate = now, CityFrom = cities.Find(x => x.Name == "London")}
            };

            flights.ForEach(x => _flighRepository.Add(x));

            flights = _flighRepository.GetAll();

            //var billingDetail = _billingRepository.Add(new BillingDetails() {DateOfBirth = user.DateOfBirth, PassportExpirationDate = user.PassportExpirationDate});

            var ticketOrders = new List<TicketOrder>()
            {
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L1"), Owner = testUser}, //London 5
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L1"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L1"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L2"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "L2"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "K1"), Owner = testUser}, //Kiev 2
                new TicketOrder() {Flight = flights.Find(x => x.Name == "K1"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "M1"), Owner = testUser}, //Moscow 3
                new TicketOrder() {Flight = flights.Find(x => x.Name == "M1"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "M1"), Owner = testUser},
                new TicketOrder() {Flight = flights.Find(x => x.Name == "P1"), Owner = testUser}, //Paris 1
                new TicketOrder() {Flight = flights.Find(x => x.Name == "D1"), Owner = testUser}, //Dublin 1
                new TicketOrder() {Flight = flights.Find(x => x.Name == "R1"), Owner = testUser}, //Rome 1
            };

            ticketOrders.ForEach(x => _orderRepository.Add(x));
        }

        [TestMethod]
        public void GetTop5PopularDestinations_WhenCalled_ReturnFivePopular()
        {
            //Arrange

            //Act
            var actualResult = _popularService.GetTop5PopularDestinations();

            //Assert
            Assert.AreEqual(5, actualResult.Count);
            Assert.AreEqual("London", actualResult[0].Name);
            Assert.AreEqual("Moscow", actualResult[1].Name);
            Assert.AreEqual("Kiev", actualResult[2].Name);
        }

        [TestCleanup()]
        public void ClearDb()
        {
            var allOrders = _orderRepository.GetAll();
            allOrders.ForEach(x => _orderRepository.Delete(x));

            //var allFlights = _flighRepository.GetAll();
            //allFlights.ForEach(x => _flighRepository.Delete(x));

            var allCities = _cityRepository.GetAll();
            allCities.ForEach(x => _cityRepository.Delete(x));

            _userRepository.Delete(testUser);
        }
    }
}
