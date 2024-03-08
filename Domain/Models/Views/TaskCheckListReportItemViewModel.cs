namespace Domain.Models.Views
{
    public class TaskCheckListReportItemViewModel
    {
        public Guid Id { get; set; }
        public Guid TaskCheckListReportId { get; set; }
        public string Issue { get; set; } = null!;
        public bool Positive { get; set; }
        public int Severity { get; set; }
        public string? Message { get; set; }
    }
}
