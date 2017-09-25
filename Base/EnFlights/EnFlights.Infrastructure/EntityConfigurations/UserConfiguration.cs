using EnFlights.ApplicationCore.Entities;
using System.Data.Entity.ModelConfiguration;

namespace EnFlights.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(x => x.Email).IsRequired().HasMaxLength(50);
            Property(x => x.Password).IsRequired().HasMaxLength(50);

            HasMany(x => x.BookedFlights).WithMany(x => x.Passengers);
            HasMany(x => x.TicketOrders);
        }
    }
}
