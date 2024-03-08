

using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class TaskSampleCreateModel
    {
        public IFormFile ThumbnailUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CareModeId { get; set; }
    }
}
