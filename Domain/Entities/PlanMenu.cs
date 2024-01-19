using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PlanMenu
    {
        public string Name { get; set; } = null!;
        public int? DayOfWeek { get; set; }
        public Guid PlanId { get; set; }
        public Guid MenuId { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid DayOfWeekId { get; set; }

        public virtual DayOfWeek DayOfWeekNavigation { get; set; } = null!;
        public virtual Menu Menu { get; set; } = null!;
        public virtual Plan Plan { get; set; } = null!;
    }
}
