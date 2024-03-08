namespace Domain.Models.Views
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public string TicketCategory { get; set; } = null!;
        public Guid CreatorId { get; set; }
        public string Priority { get; set; } = null!;
        public Guid? AssigneeId { get; set; }
        public Guid? CageId { get; set; }
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
