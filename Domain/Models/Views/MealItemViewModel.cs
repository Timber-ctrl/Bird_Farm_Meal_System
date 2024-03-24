namespace Domain.Models.Views
{
    public class MealItemViewModel
    {
        public MenuMealViewModel MenuMeal { get; set; } = null!;
        public FoodViewModel Food { get; set; } = null!;
        public double Quantity { get; set; }
        public int Order { get; set; }
    }
}
