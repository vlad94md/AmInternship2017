using EnFlights.ApplicationCore.Entities;
using System.Data.Entity.ModelConfiguration;

namespace EnFlights.Infrastructure.EntityConfigurations
{
    public class TicketOrderConfiguration : EntityTypeConfiguration<TicketOrder>
    {
        public TicketOrderConfiguration()
        {
            HasRequired(x => x.Owner);
            HasRequired(x => x.Flight);
            HasOptional(x => x.BillingDetails).WithRequired(x => x.TicketOrder);
        }
    }
}
