namespace Domain.Models.Creates
{
    public class NotificationCreateModel
    {
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Link { get; set; }
        public string? Type { get; set; }
    }
}