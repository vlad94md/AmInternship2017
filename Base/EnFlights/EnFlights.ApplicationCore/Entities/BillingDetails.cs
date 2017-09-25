using EnFlights.ApplicationCore.Entities.Base;
using System;

namespace EnFlights.ApplicationCore.Entities
{
    public class BillingDetails : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime PassportExpirationDate { get; set; }
        public string Citizenship { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Guid TicketOrderId { get; set; }

        public virtual TicketOrder TicketOrder { get; set; }
    }
}
