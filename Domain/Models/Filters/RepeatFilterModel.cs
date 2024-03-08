namespace Domain.Models.Filters
{
    public class RepeatFilterModel
    {
        public string? Type { get; set; } = null!;
        public Guid? TaskId { get; set; }
        public Guid? TaskSampleId { get; set; }
    }
}
