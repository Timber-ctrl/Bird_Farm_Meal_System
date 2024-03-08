namespace Domain.Models.Filters
{
    public class MenuSampleFilterModel
    {
        public string? Name { get; set; } = null!;
        public Guid? SpeciesId { get; set; }
        public Guid? CareModeId { get; set; }
    }
}
