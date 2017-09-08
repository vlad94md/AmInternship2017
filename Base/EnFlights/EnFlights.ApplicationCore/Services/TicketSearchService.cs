using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnFlights.ApplicationCore.Services
{
    public class TicketSearchService : ITicketSearchService
    {
        private readonly IRepository<Flight> _flightRepository;

        public TicketSearchService(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public List<Flight> FindFlights(TicketSearchModel searchCriteria)
        {
            var flights = _flightRepository.GetAll().
                Where(x => CompareDates(x.ArrivalDate, searchCriteria.ArrivalDate) &&
                           CompareDates(x.DepartureDate, searchCriteria.DepartureDate) &&
                           x.CityToId == searchCriteria.CityToId &&
                           x.CityFromId == searchCriteria.CityFromId).ToList();

            return flights;
        }

        private bool CompareDates(DateTime x, DateTime y)
        {
            return (x.Day == y.Day && x.Month == y.Month && x.Year == y.Year);
        }
    }
}