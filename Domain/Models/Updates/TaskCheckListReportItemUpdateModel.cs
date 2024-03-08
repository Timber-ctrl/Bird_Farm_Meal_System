namespace Domain.Models.Updates
{
    public class TaskCheckListReportItemUpdateModel
    {
        public Guid? TaskCheckListReportId { get; set; }
        public string? Issue { get; set; } = null!;
        public bool? Positive { get; set; }
        public int? Severity { get; set; }
        public string? Message { get; set; }
    }
}
