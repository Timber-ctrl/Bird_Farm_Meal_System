namespace Domain.Models.Views
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public string TicketCategory { get; set; } = null!;
        public StaffViewModel Creator { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public StaffViewModel Assignee { get; set; } = null!;
        public CageViewModel Cage { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
