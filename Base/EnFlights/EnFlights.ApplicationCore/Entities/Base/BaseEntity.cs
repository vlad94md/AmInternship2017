using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnFlights.ApplicationCore.Entities.Base
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  Guid Id { get; set; }
    }
}
