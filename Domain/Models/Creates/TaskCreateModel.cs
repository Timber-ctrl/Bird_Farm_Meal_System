using Microsoft.AspNetCore.Http;

namespace Domain.Models.Creates
{
    public class TaskCreateModel
    {
        public Guid CageId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Guid ManagerId { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; } = null!;
        public ICollection<Guid>? AssigneeIds { get; set; }
        public ICollection<TaskCheckListCreateModel> CheckLists { get; set; } = new List<TaskCheckListCreateModel>();
    }
}