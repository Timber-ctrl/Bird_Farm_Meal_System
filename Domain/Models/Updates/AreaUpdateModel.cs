using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class AreaUpdateModel
    {
        public string? Name { get; set; } = null!;
        public IFormFile? Thumbnail { get; set; }
    }
}
