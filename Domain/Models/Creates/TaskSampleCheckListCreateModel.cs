namespace Domain.Models.Creates
{
    public class TaskSampleCheckListCreateModel
    {
        public string Title { get; set; } = null!;
        public Guid TaskSampleId { get; set; }
        public int Order { get; set; }
    }
}
