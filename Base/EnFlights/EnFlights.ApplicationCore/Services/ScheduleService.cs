using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnFlights.ApplicationCore.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<Flight> _flightRepository;

        public ScheduleService(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public List<Flight> GetTodaylFlights()
        {
            var today = DateTime.Now;

            return _flightRepository.GetAll()
                .Where(x => (x.ArrivalDate.Day == today.Day || x.DepartureDate.Day == today.Day) &&
                (x.ArrivalDate.Month == today.Month || x.DepartureDate.Month == today.Month)  &&
                (x.ArrivalDate.Year == today.Year || x.DepartureDate.Year == today.Year))
                .ToList();
        }

        public List<Flight> GetTomorrowFlights()
        {
            var tomorrow = DateTime.Now.AddDays(1);

            return _flightRepository.GetAll()
                .Where(x => (x.ArrivalDate.Day == tomorrow.Day || x.DepartureDate.Day == tomorrow.Day) &&
                (x.ArrivalDate.Month == tomorrow.Month || x.DepartureDate.Month == tomorrow.Month) &&
                (x.ArrivalDate.Year == tomorrow.Year || x.DepartureDate.Year == tomorrow.Year))
                .ToList();
        }
    }
}