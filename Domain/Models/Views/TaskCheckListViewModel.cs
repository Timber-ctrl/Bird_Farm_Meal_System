namespace Domain.Models.Views
{
    public class TaskCheckListViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public TaskViewModel Task { get; set; } = null!;
        public StaffViewModel Asignee { get; set; } = null!;
        public bool Status { get; set; }
        public int Order { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
