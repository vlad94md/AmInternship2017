using EnFlights.ApplicationCore.Entities;
using EnFlights.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EnFlights.ApplicationCore.Services
{
    public class PopularService : IPopularService
    {
        private IRepository<TicketOrder> _orderRepository;

        public PopularService(IRepository<TicketOrder> orderRepo)
        {
            _orderRepository = orderRepo;
        }
        public List<City> GetTop5PopularDestinations()
        {
            var result = _orderRepository.GetAll().
                GroupBy(x => x.Flight.CityTo).
                OrderByDescending(x => x.Count()).
                Select(g => g.Key).
                Take(5).ToList();

            return result;
        }
    }
}