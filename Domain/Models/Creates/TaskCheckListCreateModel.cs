namespace Domain.Models.Creates
{
    public class TaskCheckListCreateModel
    {
        public string Title { get; set; } = null!;
        public Guid AsigneeId { get; set; }
        public int Order { get; set; }
    }
}
