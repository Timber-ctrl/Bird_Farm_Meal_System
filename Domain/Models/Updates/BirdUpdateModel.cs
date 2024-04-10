using Microsoft.AspNetCore.Http;

namespace Domain.Models.Updates
{
    public class BirdUpdateModel
    {
        public string? Characteristic { get; set; }
        public string? Name { get; set; } = null!;
        public bool? Gender { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string? Code { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public Guid? CageId { get; set; }
        public Guid? MenuId { get; set; }
        public Guid? SpeciesId { get; set; }
        public Guid? CareModeId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
