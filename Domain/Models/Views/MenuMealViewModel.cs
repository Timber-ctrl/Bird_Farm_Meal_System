namespace Domain.Models.Views
{
    public class MenuMealViewModel
    {
        public Guid Id { get; set; }
        public MenuViewModel Menu { get; set; } = null!;
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
