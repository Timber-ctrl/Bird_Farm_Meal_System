using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
            PlanMenus = new HashSet<PlanMenu>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid SpeciesId { get; set; }
        public Guid StageId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Species Species { get; set; } = null!;
        public virtual Stage Stage { get; set; } = null!;
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<PlanMenu> PlanMenus { get; set; }
    }
}
