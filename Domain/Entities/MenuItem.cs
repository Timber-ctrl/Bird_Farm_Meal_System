using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MenuItem
    {
        public Guid MenuId { get; set; }
        public Guid FoodId { get; set; }
        public double Quantity { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; } = null!;
    }
}
