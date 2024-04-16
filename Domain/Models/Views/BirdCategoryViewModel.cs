namespace Domain.Models.Views
{
    public class BirdCategoryViewModel
    {
        public Guid Id { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string Name { get; set; } = null!;
    }
}
