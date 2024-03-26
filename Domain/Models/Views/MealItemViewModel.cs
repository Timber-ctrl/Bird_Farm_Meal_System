namespace Domain.Models.Views
{
    public class MealItemViewModel
    {
        public Guid Id { get; set; }
        public MenuMealViewModel MenuMeal { get; set; } = null!;
        public FoodViewModel Food { get; set; } = null!;
        public double Quantity { get; set; }
        public int Order { get; set; }
    }
}
