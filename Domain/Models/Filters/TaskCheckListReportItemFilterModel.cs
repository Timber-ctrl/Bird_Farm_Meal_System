namespace Domain.Models.Filters
{
    public class TaskCheckListReportItemFilterModel
    {
        public Guid? TaskCheckListReportId { get; set; }
        public string? Issue { get; set; } = null!;
    }
}
