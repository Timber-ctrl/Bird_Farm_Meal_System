using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class BirdCategoryCreateModel
    {
        public IFormFile Thumbnail { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
