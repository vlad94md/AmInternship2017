using EnFlights.ApplicationCore.Entities;
using System.Data.Entity.ModelConfiguration;

namespace EnFlights.Infrastructure.EntityConfigurations
{
    class BillingDetailsConfiguration :  EntityTypeConfiguration<BillingDetails>
    {
        public BillingDetailsConfiguration()
        {
            HasKey(t => t.Id);
        }
    }
}
