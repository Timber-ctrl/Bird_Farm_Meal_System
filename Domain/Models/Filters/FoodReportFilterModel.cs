namespace Domain.Models.Filters
{
    public class FoodReportFilterModel
    {
        public Guid? StaffId { get; set; }
        public Guid? FoodId { get; set; }
        public double? RemainQuantity { get; set; }
    }
}