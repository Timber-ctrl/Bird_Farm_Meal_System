using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class SpeciesCreateModel
    {
        public IFormFile? Thumbnail { get; set; }
        public string Name { get; set; } = null!;
        public Guid BirdCategoryId { get; set; }
    }
}
