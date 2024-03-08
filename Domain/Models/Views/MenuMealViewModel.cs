namespace Domain.Models.Views
{
    public class MenuMealViewModel
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
