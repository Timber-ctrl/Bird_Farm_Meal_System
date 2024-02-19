using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UnitOfMeasurement
    {
        public UnitOfMeasurement()
        {
            Foods = new HashSet<Food>();
            MealItemSamples = new HashSet<MealItemSample>();
            MealItems = new HashSet<MealItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<MealItemSample> MealItemSamples { get; set; }
        public virtual ICollection<MealItem> MealItems { get; set; }
    }
}
