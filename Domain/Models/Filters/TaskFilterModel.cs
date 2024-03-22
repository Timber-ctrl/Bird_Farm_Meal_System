namespace Domain.Models.Filters
{
    public class TaskFilterModel
    {
        public string? Title { get; set; }
        public Guid? CageId { get; set; }
        public Guid? ManagerId { get; set; }
        public string? Status { get; set; }
    }
}