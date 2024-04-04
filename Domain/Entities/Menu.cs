using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            Birds = new HashSet<Bird>();
            MenuMeals = new HashSet<MenuMeal>();
            Plans = new HashSet<Plan>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<MenuMeal> MenuMeals { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
