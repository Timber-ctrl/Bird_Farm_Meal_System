namespace Domain.Models.Creates
{
    public class FoodReportCreateModel
    {
        public Guid StaffId { get; set; }
        public Guid FoodId { get; set; }
        public double RemainQuantity { get; set; }
        public string? Description { get; set; } = null!;
    }
}