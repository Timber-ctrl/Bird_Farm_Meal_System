using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class TicketUpdateModel
    {
        public string? TicketCategory { get; set; }
        public Guid? CreatorId { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid? CageId { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? ResultImage { get; set; }
        public string? ResultDescription { get; set; }
        public string? Status { get; set; }
    }
}
