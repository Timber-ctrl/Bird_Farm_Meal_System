namespace Domain.Models.Updates
{
    public class TaskSampleCheckListUpdateModel
    {
        public string? Title { get; set; } = null!;
        public Guid? TaskSampleId { get; set; }
        public int? Order { get; set; }

    }
}
