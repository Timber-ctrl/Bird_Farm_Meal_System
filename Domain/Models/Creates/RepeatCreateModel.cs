namespace Domain.Models.Creates
{
    public class RepeatCreateModel
    {
        public string Type { get; set; } = null!;
        public int Time { get; set; }
        public DateTime Until { get; set; }
    }
}
