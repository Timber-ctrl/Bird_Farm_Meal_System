namespace Domain.Models.Creates
{
    public class TaskCheckListCreateModel
    {
        public string? Title { get; set; }
        public Guid TaskId { get; set; }
        public Guid AsigneeId { get; set; }
        public int Order { get; set; }
    }
}
