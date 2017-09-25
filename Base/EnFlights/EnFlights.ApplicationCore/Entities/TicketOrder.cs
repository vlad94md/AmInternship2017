using EnFlights.ApplicationCore.Entities.Base;
using EnFlights.ApplicationCore.Entities.Constants;
using System;

namespace EnFlights.ApplicationCore.Entities
{
    public class TicketOrder : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid FlightId { get; set; }
        public Guid BillingDetailsId { get; set; }
        public int NumberOfSeats { get; set; }
        public BaggageTypeEnum BaggageType { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User Owner { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual BillingDetails BillingDetails { get; set; }


        public TicketOrder()
        {
            OrderDate = DateTime.Now;
        }
    }
}