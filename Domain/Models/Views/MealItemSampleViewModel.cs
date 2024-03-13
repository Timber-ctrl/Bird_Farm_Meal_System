namespace Domain.Models.Views
{
    public class MealItemSampleViewModel
    {
        public MenuMealSampleViewModel MenuMealSample { get; set; } = null!;
        public FoodViewModel Food { get; set; } = null!;
        public double Quantity { get; set; }
        public int Order { get; set; }
    }
}
