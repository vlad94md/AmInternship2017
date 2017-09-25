using System;

namespace EnFlights.ApplicationCore.Models
{
    public class TicketSearchModel
    {
        public Guid CityFromId { get; set; }
        public DateTime DepartureDate { get; set; }
        public Guid CityToId { get; set; }
        public DateTime? BackDate { get; set; }
        public bool IsRoundWay { get; set; }
        public int NumberOfPassengers { get; set; }
    }
}