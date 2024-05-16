namespace Domain.Models.Filters
{
    public class TaskFilterModel
    {
        public string? Title { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? FarmId { get; set; }
        public string? Status { get; set; }
    }
}