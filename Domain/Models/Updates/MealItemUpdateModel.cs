namespace Domain.Models.Updates
{
    public class MealItemUpdateModel
    {
        public Guid? FoodId { get; set; }
        public double? Quantity { get; set; }
        public int? Order { get; set; }
    }
}
