namespace Domain.Models.Views
{
    public class MealItemSampleViewModel
    {
        public Guid Id { get; set; }
        public FoodViewModel Food { get; set; } = null!;
        public double Quantity { get; set; }
        public int Order { get; set; }
    }
}
