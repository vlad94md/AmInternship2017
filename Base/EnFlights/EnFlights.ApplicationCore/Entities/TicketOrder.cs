using EnFlights.ApplicationCore.Entities.Base;
using EnFlights.ApplicationCore.Entities.Constants;
using System;

namespace EnFlights.ApplicationCore.Entities
{
    public class TicketOrder : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User Owner { get; set; }
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
        public int NumberOfSeats { get; set; }
        public BaggageTypeEnum BaggageType { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}