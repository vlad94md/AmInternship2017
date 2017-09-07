using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnFlights.Tests.ApplicationCore
{
    [TestClass]
    public class ScheduleServiceTests
    {
        private ScheduleService _scheduleService;
        private Mock<IRepository<Flight>> _mockRepository;

        private DateTime pastDate = new DateTime(2017, 1, 1);
        private DateTime futureDate = new DateTime(2020, 1, 1);
        private DateTime tomorrow = DateTime.Now.AddDays(1);
        private DateTime today = DateTime.Now;

        [TestInitialize]
        public void TestInialize()
        {
            _mockRepository = new Mock<IRepository<Flight>>();
        }

        [TestMethod]
        public void GetTodaylFlights_TodayArrivalDateFlightsExist_ReturnFlights()
        {
            //Arrange
            var flights = new List<Flight>
            {
                new Flight()
                {
                    Name = "FA536", ArrivalDate = today
                },
                new Flight()
                {
                    Name = "FA678", ArrivalDate = pastDate
                },
                new Flight()
                {
                    Name = "FJ190", ArrivalDate = futureDate
                }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(flights);
            _scheduleService = new ScheduleService(_mockRepository.Object);

            //Act
            var actualResult = _scheduleService.GetTodaylFlights();

            //Assert
            Assert.AreEqual(1, actualResult.Count);
            Assert.AreEqual(flights.First().Name, actualResult.First().Name);
        }

        [TestMethod]
        public void GetTodaylFlights_TodayDepartureDateFlightsExist_ReturnFlights()
        {
            //Arrange
            var flights = new List<Flight>
            {
                new Flight()
                {
                    Name = "FA536", DepartureDate = today, ArrivalDate = futureDate
                },
                new Flight()
                {
                    Name = "FJ190", DepartureDate = futureDate, ArrivalDate = futureDate
                }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(flights);
            _scheduleService = new ScheduleService(_mockRepository.Object);

            //Act
            var actualResult = _scheduleService.GetTodaylFlights();

            //Assert
            Assert.AreEqual(1, actualResult.Count);
            Assert.AreEqual(flights.First().Name, actualResult.First().Name);
        }

        [TestMethod]
        public void GetTodayFlights_TodayFlightsDontExist_ReturnEmptyList()
        {
            //Arrange
            var flights = new List<Flight>
            {
                new Flight()
                {
                    Name = "FA536", ArrivalDate = pastDate, DepartureDate = pastDate
                }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(flights);
            _scheduleService = new ScheduleService(_mockRepository.Object);

            //Act
            var actualResult = _scheduleService.GetTodaylFlights();

            //Assert
            Assert.AreEqual(0, actualResult.Count);
        }

        [TestMethod]
        public void GetTomorrowFlights_TomorrowArrivalDateFlightsExist_ReturnFlights()
        {
            //Arrange
            var flights = new List<Flight>
            {
                new Flight()
                {
                    Name = "FA536", ArrivalDate = tomorrow
                },
                new Flight()
                {
                    Name = "FA678", ArrivalDate = pastDate
                },
                new Flight()
                {
                    Name = "FJ190", ArrivalDate = futureDate
                }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(flights);
            _scheduleService = new ScheduleService(_mockRepository.Object);

            //Act
            var actualResult = _scheduleService.GetTomorrowFlights();

            //Assert
            Assert.AreEqual(1, actualResult.Count);
            Assert.AreEqual(flights.First().Name, actualResult.First().Name);
        }

        [TestMethod]
        public void GetTomorrowFlights_TomorrowDepartureDateFlightsExist_ReturnFlights()
        {
            //Arrange
            var flights = new List<Flight>
            {
                new Flight()
                {
                    Name = "FA536", DepartureDate = tomorrow, ArrivalDate = futureDate
                },
                new Flight()
                {
                    Name = "FJ190", DepartureDate = futureDate, ArrivalDate = futureDate
                }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(flights);
            _scheduleService = new ScheduleService(_mockRepository.Object);

            //Act
            var actualResult = _scheduleService.GetTomorrowFlights();

            //Assert
            Assert.AreEqual(1, actualResult.Count);
            Assert.AreEqual(flights.First().Name, actualResult.First().Name);
        }

        [TestMethod]
        public void GetTomorrowFlights_OnlyTodayFlightsExist_ReturnEmptyList()
        {
            //Arrange
            var flights = new List<Flight>
            {
                new Flight()
                {
                    Name = "FA536", ArrivalDate = today, DepartureDate = today
                }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(flights);
            _scheduleService = new ScheduleService(_mockRepository.Object);

            //Act
            var actualResult = _scheduleService.GetTomorrowFlights();

            //Assert
            Assert.AreEqual(0, actualResult.Count);
        }
    }
}
