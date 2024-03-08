namespace Domain.Models.Views
{
    public class TaskCheckListReportViewModel
    {
        public Guid Id { get; set; }
        public Guid TaskCheckListId { get; set; }
        public DateTime FinishAt { get; set; }
    }
}
