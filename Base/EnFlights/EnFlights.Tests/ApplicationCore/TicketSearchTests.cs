using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Models;
using EnFlights.ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class TicketSearchTests
    {
        private ITicketSearchService _ticketService;
        private Mock<IRepository<Flight>> _mockRepository;

        private Guid MoscowCityId;
        private Guid LondonCityId;

        [TestInitialize]
        public void TestInialize()
        {
            MoscowCityId = Guid.NewGuid();
            LondonCityId = Guid.NewGuid();

            _mockRepository = new Mock<IRepository<Flight>>();

            var cities = new List<Flight>()
            {
                new Flight() { DepartureDate = DateTime.Now.AddDays(1), CityFromId = MoscowCityId, CityToId = LondonCityId, BookedSeats = 10, TotalSeats = 11},
                new Flight() { DepartureDate = DateTime.Now.AddDays(2), CityFromId = MoscowCityId, CityToId = LondonCityId, BookedSeats = 10, TotalSeats = 11},
                new Flight() { DepartureDate = DateTime.Now.AddDays(3), CityFromId = LondonCityId, CityToId =  MoscowCityId, BookedSeats = 10, TotalSeats = 11},
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(cities);
            _ticketService = new TicketSearchService(_mockRepository.Object);
        }


        [TestMethod]
        public void FindFlights_OneWayAndFlightsExist_ReturnFlights()
        {
            //Arrange
            var searchCriteria = new TicketSearchModel()
            {
                IsRoundWay = false,
                DepartureDate = DateTime.Now.AddDays(1),
                BackDate = null,
                CityFromId = MoscowCityId,
                CityToId = LondonCityId,
                NumberOfPassengers = 1
            };


            //Act
            var actualResult = _ticketService.FindFlights(searchCriteria);

            //Assert
            Assert.AreEqual(1, actualResult.ToFlights.Count);
            Assert.AreEqual(0, actualResult.ReturnFlights.Count);
        }

        [TestMethod]
        public void FindFlights_OneWayAndFlightsDontExist_ReturnEmpty()
        {
            //Arrange
            var searchCriteria = new TicketSearchModel()
            {
                IsRoundWay = false,
                DepartureDate = DateTime.Now.AddDays(10),
                BackDate = null,
                CityFromId = MoscowCityId,
                CityToId = LondonCityId,
                NumberOfPassengers = 1
            };


            //Act
            var actualResult = _ticketService.FindFlights(searchCriteria);

            //Assert
            Assert.AreEqual(0, actualResult.ToFlights.Count);
            Assert.AreEqual(0, actualResult.ReturnFlights.Count);
        }

        [TestMethod]
        public void FindFlights_RoundWayAndFlightsExist_ReturnToAndFromFlights()
        {
            //Arrange
            var searchCriteria = new TicketSearchModel()
            {
                IsRoundWay = true,
                DepartureDate = DateTime.Now.AddDays(1),
                BackDate = DateTime.Now.AddDays(3),
                CityFromId = MoscowCityId,
                CityToId = LondonCityId,
                NumberOfPassengers = 1
            };


            //Act
            var actualResult = _ticketService.FindFlights(searchCriteria);

            //Assert
            Assert.AreEqual(1, actualResult.ToFlights.Count);
            Assert.AreEqual(1, actualResult.ReturnFlights.Count);
        }

        [TestMethod]
        public void FindFlights_RoundWayAndFlightsDontExist_ReturnEmptyToFlights()
        {
            //Arrange
            var searchCriteria = new TicketSearchModel()
            {
                IsRoundWay = true,
                DepartureDate = DateTime.Now.AddDays(3),
                BackDate = DateTime.Now.AddDays(3),
                CityFromId = MoscowCityId,
                CityToId = LondonCityId,
                NumberOfPassengers = 1
            };


            //Act
            var actualResult = _ticketService.FindFlights(searchCriteria);

            //Assert
            Assert.AreEqual(0, actualResult.ToFlights.Count);
            Assert.AreEqual(1, actualResult.ReturnFlights.Count);
        }

        [TestMethod]
        public void FindFlights_NotEnoughPlaces_ReturnEmpty()
        {
            //Arrange
            var searchCriteria = new TicketSearchModel()
            {
                IsRoundWay = true,
                DepartureDate = DateTime.Now.AddDays(1),
                BackDate = DateTime.Now.AddDays(3),
                CityFromId = MoscowCityId,
                CityToId = LondonCityId,
                NumberOfPassengers = 2
            };


            //Act
            var actualResult = _ticketService.FindFlights(searchCriteria);

            //Assert
            Assert.AreEqual(0, actualResult.ToFlights.Count);
            Assert.AreEqual(0, actualResult.ReturnFlights.Count);
        }

    }
}
