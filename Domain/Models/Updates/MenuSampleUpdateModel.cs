namespace Domain.Models.Updates
{
    public class MenuSampleUpdateModel
    {
        public string? Name { get; set; } = null!;
        public Guid? SpeciesId { get; set; }
        public Guid? CareModeId { get; set; }
    }
}
