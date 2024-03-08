namespace Domain.Models.Views
{
    public class TaskSampleViewModel
    {
        public Guid Id { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CareModeId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
