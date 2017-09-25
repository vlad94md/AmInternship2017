using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Entities.Constants;
using EnFlights.ApplicationCore.Exceptions;
using EnFlights.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnFlights.ApplicationCore.Services
{
    public class TicketOrderService : ITicketOrderService
    {
        private readonly IRepository<TicketOrder> _orderRepository;
        private readonly IRepository<Flight> _flightRepository;

        private decimal standardBaggagePrice = (decimal) 10.0;
        private decimal extraBaggagePrice = (decimal) 40.0;


        public TicketOrderService(IRepository<TicketOrder> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<TicketOrder> GetUserBookings(string username)
        {
            return _orderRepository.GetAll().Where(x => x.Owner.Email == username).ToList();
        }

        public TicketOrder CreateTicketsOrder(Guid flightId, int numberOfSeats, BaggageTypeEnum baggageType, User owner)
        {
            if(owner == null) { throw new ArgumentException("Owner data is invalid"); }

            var flight = _flightRepository.GetById(flightId);

            if (flight == null) { throw new ArgumentException("Flight was not found"); }

            if (flight.BookedSeats + numberOfSeats > flight.TotalSeats)
            {
                throw new OrderFailException("Not enough places available to book this flight");
            }

            if (owner.PassportExpirationDate < DateTime.Now)
            {
                throw new OrderFailException("Passport of a passanger is expired. Check expiration date");
            }

            decimal calculatedPrice = CalculatePrice(flight, numberOfSeats, baggageType);

            var order = new TicketOrder()
            {
                Flight = flight,
                BaggageType = baggageType,
                NumberOfSeats = numberOfSeats,
                OrderDate = DateTime.Now,
                Owner = owner,
                TotalPrice = calculatedPrice
            };

            var createdOrder =_orderRepository.Add(order);

            return createdOrder;
        }

        private decimal CalculatePrice(Flight flight, int numberOfSeats, BaggageTypeEnum baggageType)
        {
            decimal calculatedPrice = flight.TicketPrice * numberOfSeats;

            if (baggageType == BaggageTypeEnum.Standard)
            {
                calculatedPrice = standardBaggagePrice;
            }
            if (baggageType == BaggageTypeEnum.Standard)
            {
                calculatedPrice = extraBaggagePrice;
            }

            return calculatedPrice;
        }
    }
}