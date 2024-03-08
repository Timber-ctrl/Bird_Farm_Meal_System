namespace Domain.Models.Views
{
    public class MenuSampleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid SpeciesId { get; set; }
        public Guid CareModeId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
