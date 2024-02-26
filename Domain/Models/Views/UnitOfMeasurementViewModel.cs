namespace Domain.Models.Views
{
    public class UnitOfMeasurementViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
