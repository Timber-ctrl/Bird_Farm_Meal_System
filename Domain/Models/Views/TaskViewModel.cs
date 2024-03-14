namespace Domain.Models.Views
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public CageViewModel Cage { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ManagerViewModel Manager { get; set; } = null!;
        public DateTime DeadLine { get; set; }
        public DateTime CreateAt { get; set; }
        public string Status { get; set; } = null!;
    }
}