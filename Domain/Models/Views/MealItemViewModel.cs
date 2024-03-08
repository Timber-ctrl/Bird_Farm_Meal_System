namespace Domain.Models.Views
{
    public class MealItemViewModel
    {
        public Guid MenuMealId { get; set; }
        public Guid FoodId { get; set; }
        public double Quantity { get; set; }
        public Guid UnitOfMeasurementId { get; set; }
        public int Order { get; set; }
    }
}
