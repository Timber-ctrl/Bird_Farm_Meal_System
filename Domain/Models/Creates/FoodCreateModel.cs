using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class FoodCreateModel
    {
        public IFormFile Thumbnail { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid FoodCategoryId { get; set; }
        public double Quantity { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
    }
}
