namespace Domain.Models.Filters
{
    public class BirdFilterModel
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CageId { get; set; }
        public Guid? SpeciesId { get; set; }
        public Guid? FarmId { get; set; }
    }
}
