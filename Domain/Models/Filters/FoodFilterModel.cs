namespace Domain.Models.Filters
{
    public class FoodFilterModel
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public Guid? FarmId { get; set; }
    }
}
