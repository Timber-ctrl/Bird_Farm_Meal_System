using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            MenuMeals = new HashSet<MenuMeal>();
            PlanCustomMenus = new HashSet<PlanCustomMenu>();
            Plans = new HashSet<Plan>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid SpeciesId { get; set; }
        public Guid CareModeId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual CareMode CareMode { get; set; } = null!;
        public virtual Species Species { get; set; } = null!;
        public virtual ICollection<MenuMeal> MenuMeals { get; set; }
        public virtual ICollection<PlanCustomMenu> PlanCustomMenus { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
