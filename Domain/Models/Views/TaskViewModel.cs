using Domain.Entities;

namespace Domain.Models.Views
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public ManagerViewModel Manager { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime CreateAt { get; set; }
        public ICollection<TaskCheckListViewModel> CheckLists { get; set; } = new List<TaskCheckListViewModel>();
        public ICollection<AssignStaffViewModel> AssignStaffs { get; set; } = new List<AssignStaffViewModel>();
        public string Status { get; set; } = null!;
    }
}