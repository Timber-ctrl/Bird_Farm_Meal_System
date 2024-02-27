using Domain.Entities;

namespace Domain.Models.Views
{
    public class FoodViewModel
    {
        public Guid Id { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public FoodCategoryViewModel FoodCategory { get; set; } = null!;
        public double Quantity { get; set; }
        public UnitOfMeasurementViewModel UnitOfMeasurement { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
