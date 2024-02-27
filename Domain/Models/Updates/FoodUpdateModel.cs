using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class FoodUpdateModel
    {
        public IFormFile? Thumbnail { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public Guid? FoodCategoryId { get; set; }
        public double? Quantity { get; set; }
        public Guid? UnitOfMeasurementId { get; set; }
        public string? Status { get; set; } = null!;
    }
}
