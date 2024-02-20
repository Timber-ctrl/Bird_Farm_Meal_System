using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class SpeciesUpdateModel
    {
        public IFormFile? Thumbnail { get; set; }
        public string? Name { get; set; } = null!;
        public Guid? BirdCategoryId { get; set; }
    }
}
