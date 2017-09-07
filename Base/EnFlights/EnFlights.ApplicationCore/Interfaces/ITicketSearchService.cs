using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Models;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface ITicketSearchService
    {
        List<Flight> FindFlights(TicketSearchModel searchCriteria);
    }
}
