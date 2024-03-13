namespace Domain.Models.Views
{
    public class RepeatViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public int Time { get; set; }
        public DateTime Until { get; set; }
        public TaskViewModel Task { get; set; } = null!;
        public TaskSampleViewModel TaskSample { get; set; } = null!;
    }
}
