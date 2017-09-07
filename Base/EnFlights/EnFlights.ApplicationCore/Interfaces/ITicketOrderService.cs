using EnFlights.ApplicationCore.Entities;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface ITicketOrderService
    {

        List<TicketOrder> GetUserBookings();
    }
}
