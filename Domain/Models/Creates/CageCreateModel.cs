using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class CageCreateModel
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string? Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public Guid AreaId { get; set; }
    }
}