namespace Domain.Models.Updates
{
    public class MenuMealSampleUpdateModel
    {
        public string? Name { get; set; } = null!;
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
    }
}
