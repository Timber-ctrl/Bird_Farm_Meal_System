namespace Domain.Models.Views
{
    public class MenuSampleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public SpeciesViewModel Species { get; set; } = null!;
        public CareModeViewModel CareMode { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
