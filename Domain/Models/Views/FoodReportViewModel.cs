namespace Domain.Models.Views
{
    public class FoodReportViewModel
    {
        public Guid Id { get; set; }
        public StaffViewModel Staff { get; set; } = null!;
        public FoodViewModel Food { get; set; } = null!;
        public double LastQuantity { get; set; }
        public double RemainQuantity { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}