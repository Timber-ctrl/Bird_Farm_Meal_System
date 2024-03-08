namespace Domain.Models.Views
{
    public class TaskSampleCheckListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid TaskSampleId { get; set; }
        public int Order { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
