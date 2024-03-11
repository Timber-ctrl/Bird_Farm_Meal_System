namespace Domain.Models.Creates
{
    public class MealItemSampleCreateModel
    {
        public Guid MenuMealSampleId { get; set; }
        public Guid FoodId { get; set; }
        public double Quantity { get; set; }
        public int Order { get; set; }
    }
}
