using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class AreaCreateModel
    {
        public string Name { get; set; } = null!;
        public IFormFile? Thumbnail { get; set; }
        public Guid FarmId { get; set; }
    }
}
