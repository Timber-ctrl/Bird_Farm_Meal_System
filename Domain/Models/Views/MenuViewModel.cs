using Domain.Entities;

namespace Domain.Models.Views
{
    public class MenuViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public ICollection<MenuMealViewModel> MenuMeals { get; set; } = new List<MenuMealViewModel>();

    }
}
