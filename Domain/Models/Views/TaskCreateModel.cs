namespace Domain.Models.Views
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid CageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ManagerViewModel? Manager { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime CreateAt { get; set; }
    }
}