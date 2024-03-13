namespace Domain.Models.Views
{
    public class TaskCheckListReportViewModel
    {
        public Guid Id { get; set; }
        public TaskCheckListViewModel TaskCheckList { get; set; } = null!;
        public DateTime FinishAt { get; set; }
    }
}
