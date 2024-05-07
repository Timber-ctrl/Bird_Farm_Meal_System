namespace Domain.Models.Views
{
    public class CageViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string? Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public int NumberOfBird { get; set; }
        public string? ThumbnailUrl { get; set; }
        public AreaViewModel Area { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
