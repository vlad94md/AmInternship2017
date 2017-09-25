using EnFlights.ApplicationCore.Models;
using EnFlights.ApplicationCore.Services;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface ITicketSearchService
    {
        TicketSearchResult FindFlights(TicketSearchModel searchCriteria);
    }
}
