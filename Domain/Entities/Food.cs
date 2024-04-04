using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Food
    {
        public Food()
        {
            FoodReports = new HashSet<FoodReport>();
            MealItemSamples = new HashSet<MealItemSample>();
            MealItems = new HashSet<MealItem>();
        }

        public Guid Id { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid FoodCategoryId { get; set; }
        public double Quantity { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual FoodCategory FoodCategory { get; set; } = null!;
        public virtual UnitOfMeasurement UnitOfMeasurement { get; set; } = null!;
        public virtual ICollection<FoodReport> FoodReports { get; set; }
        public virtual ICollection<MealItemSample> MealItemSamples { get; set; }
        public virtual ICollection<MealItem> MealItems { get; set; }
    }
}
