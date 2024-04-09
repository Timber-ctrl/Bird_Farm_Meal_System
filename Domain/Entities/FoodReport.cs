using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class FoodReport
    {
        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public Guid FoodId { get; set; }
        public double LastQuantity { get; set; }
        public double RemainQuantity { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual Staff Staff { get; set; } = null!;
    }
}
