namespace Domain.Models.Creates
{
    public class RepeatCreateModel
    {
        public string Type { get; set; } = null!;
        public int Time { get; set; }
        public DateTime Until { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? TaskSampleId { get; set; }
    }
}
