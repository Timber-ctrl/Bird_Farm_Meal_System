using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class BirdCategoryUpdateModel
    {
        public IFormFile? Thumbnail { get; set; }
        public string? Name { get; set; } = null!;
    }
}
