using EnFlights.ApplicationCore.Entities;
using System.Data.Entity.ModelConfiguration;

namespace EnFlights.Infrastructure.EntityConfigurations
{
    public class FlightConfiguration : EntityTypeConfiguration<Flight>
    {
        public FlightConfiguration()
        {
            HasRequired(x => x.CityFrom).WithMany(x => x.TakeOfflights).WillCascadeOnDelete(false);
            HasRequired(x => x.CityTo).WithMany(x => x.ArrivalFlights).WillCascadeOnDelete(false);

            HasMany(x => x.Passengers).WithMany(x => x.BookedFlights);
            HasMany(x => x.TicketOrders).WithRequired(x => x.Flight);
        }
    }
}
