using EnFlights.ApplicationCore.Entities;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Models
{
    public class TicketSearchResult
    {
        public List<Flight> ToFlights { get; set; }
        public List<Flight> ReturnFlights { get; set; }

        public TicketSearchResult()
        {
            ToFlights = new List<Flight>();
            ReturnFlights = new List<Flight>();
        }
    }
}