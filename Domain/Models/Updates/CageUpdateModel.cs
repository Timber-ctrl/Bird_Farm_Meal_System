using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class CageUpdateModel
    {
        public string? Material { get; set; } = null!;
        public string? Description { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Depth { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public Guid? CareModeId { get; set; }
        public Guid? SpeciesId { get; set; }
        public Guid? AreaId { get; set; }
    }
}
