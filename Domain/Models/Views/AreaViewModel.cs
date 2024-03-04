namespace Domain.Models.Views
{
    public class AreaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ThumbnailUrl { get; set; } 
        public DateTime CreateAt { get; set; }
    }
}
