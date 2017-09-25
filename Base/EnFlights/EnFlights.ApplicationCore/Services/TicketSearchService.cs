using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.ApplicationCore.Models;
using System;
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

        public TicketSearchResult FindFlights(TicketSearchModel searchCriteria)
        {
            var result = new TicketSearchResult();

            result.ToFlights = _flightRepository.GetAll().
                Where(x => CompareDates(x.DepartureDate, searchCriteria.DepartureDate) &&
                           x.CityToId == searchCriteria.CityToId &&
                           x.CityFromId == searchCriteria.CityFromId &&
                           x.BookedSeats + searchCriteria.NumberOfPassengers <= x.TotalSeats)
                           .OrderByDescending(x => x.DepartureDate)
                           .ToList();

            if (searchCriteria.IsRoundWay)
            {
                result.ReturnFlights = _flightRepository.GetAll().
                Where(x => CompareDates(x.DepartureDate, searchCriteria.BackDate.Value) &&
                           x.CityToId == searchCriteria.CityFromId &&
                           x.CityFromId == searchCriteria.CityToId &&
                           x.BookedSeats + searchCriteria.NumberOfPassengers <= x.TotalSeats)
                           .OrderByDescending(x => x.DepartureDate)
                           .ToList();
            }

            return result;
        }

        private bool CompareDates(DateTime x, DateTime y)
        {
            return (x.Day == y.Day && x.Month == y.Month && x.Year == y.Year);
        }
    }
}