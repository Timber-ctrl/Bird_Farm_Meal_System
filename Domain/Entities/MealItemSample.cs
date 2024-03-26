using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MealItemSample
    {
        public Guid Id { get; set; }
        public Guid MenuMealSampleId { get; set; }
        public Guid FoodId { get; set; }
        public double Quantity { get; set; }
        public int Order { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual MenuMealSample MenuMealSample { get; set; } = null!;
    }
}
