using EnFlights.ApplicationCore.Entities;
using EnFlights.Infrastructure.EntityConfigurations;
using System.Data.Entity;

namespace EnFlights.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connection) : base(connection)
        {

        }

        public ApplicationDbContext() : base("DefaultConnection") 
        {
           
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TicketOrder> Orders { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new FlightConfiguration());
            modelBuilder.Configurations.Add(new TicketOrderConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new BillingDetailsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
