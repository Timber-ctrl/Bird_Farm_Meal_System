using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MealItem
    {
        public Guid Id { get; set; }
        public Guid MenuMealId { get; set; }
        public Guid FoodId { get; set; }
        public double Quantity { get; set; }
        public int Order { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual MenuMeal MenuMeal { get; set; } = null!;
    }
}
