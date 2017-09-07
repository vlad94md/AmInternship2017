using EnFlights.ApplicationCore.Entities;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface IScheduleService
    {
        List<Flight> GetTodaylFlights();
        List<Flight> GetTomorrowFlights();
    }
}
