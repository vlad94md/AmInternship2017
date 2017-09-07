using EnFlights.ApplicationCore.Entities;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface IPopularService
    {
        List<City> GetTop5PopularDestinations();
    }
}
