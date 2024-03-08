namespace Domain.Models.Creates
{
    public class MenuMealCreateModel
    {
        public Guid MenuId { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

    }
}
