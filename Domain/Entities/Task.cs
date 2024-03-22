namespace Domain.Entities
{
    public partial class Task
    {
        public Task()
        {
            AssignStaffs = new HashSet<AssignStaff>();
            Repeats = new HashSet<Repeat>();
            TaskCheckLists = new HashSet<TaskCheckList>();
        }

        public Guid Id { get; set; }
        public Guid CageId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ManagerId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreateAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Cage Cage { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null!;
        public virtual ICollection<AssignStaff> AssignStaffs { get; set; }
        public virtual ICollection<Repeat> Repeats { get; set; }
        public virtual ICollection<TaskCheckList> TaskCheckLists { get; set; }
    }
}
