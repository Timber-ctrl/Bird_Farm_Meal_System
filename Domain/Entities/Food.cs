using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Food
    {
        public Food()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public Guid Id { get; set; }
        public string AvatarUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid FoodCategoryId { get; set; }
        public double Quantity { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual FoodCategory FoodCategory { get; set; } = null!;
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; } = null!;
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
