namespace Domain.Models.Updates
{
    public class TaskCheckListUpdateModel
    {
        public string? Title { get; set; }
        public Guid? AsigneeId { get; set; }
        public int? Order { get; set; }
    }
}
