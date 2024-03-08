namespace Domain.Models.Views
{
    public class MenuMealSampleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
