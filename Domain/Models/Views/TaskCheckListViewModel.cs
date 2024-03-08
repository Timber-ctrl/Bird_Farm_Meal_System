namespace Domain.Models.Views
{
    public class TaskCheckListViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public Guid TaskId { get; set; }
        public Guid AsigneeId { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
