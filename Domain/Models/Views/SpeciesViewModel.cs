namespace Domain.Models.Views
{
    public class SpeciesViewModel
    {
        public Guid Id { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
