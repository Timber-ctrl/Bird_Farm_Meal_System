namespace Domain.Models.Updates
{
    public class TaskUpdateModel
    {
        public string? Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? StartAt { get; set; }
        public double? WorkingHours { get; set; }
        public string? Status { get; set; } = null!;
    }
}