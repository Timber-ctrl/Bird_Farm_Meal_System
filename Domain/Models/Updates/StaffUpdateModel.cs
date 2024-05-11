using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class StaffUpdateModel
    {
        public string? Name { get; set; } = null!;
        public IFormFile? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; } = null!;
        public string? Status { get; set; } = null!;
    }
}
