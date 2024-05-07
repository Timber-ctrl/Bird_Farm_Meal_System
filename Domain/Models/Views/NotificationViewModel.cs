namespace Domain.Models.Views
{
    public class NotificationViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string? Link { get; set; }

        public string? Type { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
