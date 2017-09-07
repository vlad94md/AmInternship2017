using EnFlights.ApplicationCore.Entities.Base;
using EnFlights.ApplicationCore.Entities.Constants;
using System;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Entities
{
    public class Flight : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityFromId { get; set; }
        public DateTime DepartureDate { get; set; }
        public Guid CityToId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal TicketPrice { get; set; }
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public FlightStatusEnum Status { get; set; }
        public City CityFrom { get; set; }
        public City CityTo { get; set; }
        public ICollection<User> Passengers { get; set; }
    }
}