using EnFlights.ApplicationCore.Entities.Base;
using EnFlights.ApplicationCore.Entities.Constants;
using System;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TitleEnum Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime PassportExpirationDate { get; set; }
        public string Citizenship { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Flight> BookedFlights { get; set; }
    }
}
