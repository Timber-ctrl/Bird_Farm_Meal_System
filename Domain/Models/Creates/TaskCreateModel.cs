using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class TaskCreateModel
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Guid ManagerId { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public ICollection<Guid>? AssigneeIds { get; set; }
        public ICollection<RepeatCreateModel>? Repeats { get; set; }
        public ICollection<TaskCheckListCreateModel> CheckLists { get; set; } = new List<TaskCheckListCreateModel>();
    }
}