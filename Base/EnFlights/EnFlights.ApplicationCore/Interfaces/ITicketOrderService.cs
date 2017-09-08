using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Entities.Constants;
using System;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface ITicketOrderService
    {
        List<TicketOrder> GetUserBookings(string username);
        TicketOrder CreateTicketsOrder(Guid flightId, int numberOfSeats, BaggageTypeEnum baggageType, User owner);
    }
}
