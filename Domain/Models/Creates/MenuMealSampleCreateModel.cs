namespace Domain.Models.Creates
{
    public class MenuMealSampleCreateModel
    {
        public Guid MenuSampleId { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
