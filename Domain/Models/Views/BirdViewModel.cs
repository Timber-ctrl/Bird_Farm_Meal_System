namespace Domain.Models.Views
{
    public class BirdViewModel
    {
        public Guid Id { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Characteristic { get; set; }
        public string Name { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string? Code { get; set; }
        public CageViewModel Cage { get; set; } = null!;
        public MenuViewModel? Menu { get; set; } = null!;
        public SpeciesViewModel Species { get; set; } = null!;
        public CareModeViewModel CareMode { get; set; } = null!;
        public BirdCategoryViewModel Category { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
