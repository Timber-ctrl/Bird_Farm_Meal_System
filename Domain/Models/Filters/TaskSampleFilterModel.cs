namespace Domain.Models.Filters
{
    public class TaskSampleFilterModel
    {
        public string? Name { get; set; } = null!;
        public Guid? CareModeId { get; set; }
    }
}
