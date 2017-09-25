using EnFlights.ApplicationCore.Entities;
using System.Data.Entity.ModelConfiguration;

namespace EnFlights.Infrastructure.EntityConfigurations
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            HasMany(x => x.TakeOfflights).WithRequired(x => x.CityFrom);
            HasMany(x => x.ArrivalFlights).WithRequired(x => x.CityTo);
        }
    }
}
