using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MenuMeal
    {
        public MenuMeal()
        {
            MealItems = new HashSet<MealItem>();
        }

        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual ICollection<MealItem> MealItems { get; set; }
    }
}
