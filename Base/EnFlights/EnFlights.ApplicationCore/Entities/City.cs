using EnFlights.ApplicationCore.Entities.Base;
using System;

namespace EnFlights.ApplicationCore.Entities
{
    public class City : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public byte[] Image { get; set; }
    }
}