using Domain.Entities;

namespace Domain.Models.Views
{
    public class MenuMealViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public ICollection<MealItemViewModel> MealItems { get; set; } = new List<MealItemViewModel>();
        public DateTime CreateAt { get; set; }
    }
}
