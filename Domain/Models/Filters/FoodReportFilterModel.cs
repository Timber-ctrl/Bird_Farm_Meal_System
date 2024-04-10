namespace Domain.Models.Filters
{
    public class FoodReportFilterModel
    {
        public string? Name { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? FoodId { get; set; }
        public double? RemainQuantity { get; set; }
    }
}