using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class DayOfWeek
    {
        public DayOfWeek()
        {
            PlanMenus = new HashSet<PlanMenu>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int DayNumber { get; set; }

        public virtual ICollection<PlanMenu> PlanMenus { get; set; }
    }
}
