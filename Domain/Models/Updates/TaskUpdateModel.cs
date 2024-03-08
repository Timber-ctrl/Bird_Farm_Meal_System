namespace Domain.Models.Updates
{
    public class TaskUpdateModel
    {
        public Guid? CageId { get; set; }
        public string? Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}