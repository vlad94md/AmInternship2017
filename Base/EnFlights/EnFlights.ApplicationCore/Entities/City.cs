using EnFlights.ApplicationCore.Entities.Base;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Flight> TakeOfflights { get; set; }
        public virtual ICollection<Flight> ArrivalFlights { get; set; }
    }
}