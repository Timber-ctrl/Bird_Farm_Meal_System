using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class FarmUpdateModel
    {
        public string? Name { get; set; } = null!;
        public IFormFile? Thumbnail { get; set; }
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public Guid? ManagerId { get; set; }
    }
}
