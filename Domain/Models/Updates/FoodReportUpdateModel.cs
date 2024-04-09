namespace Domain.Models.Updates
{
    public class FoodReportUpdateModel
    {
        public Guid? StaffId { get; set; }
        public Guid? FoodId { get; set; }
        public double? LastQuantity { get; set; }
        public double? RemainQuantity { get; set; }
        public string? Description { get; set; } = null!;
    }
}