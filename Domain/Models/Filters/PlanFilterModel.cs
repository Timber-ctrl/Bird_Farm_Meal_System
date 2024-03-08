namespace Domain.Models.Filters
{
    public class PlanFilterModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Guid? MenuId { get; set; }
        public Guid? CageId { get; set; }
    }
}
