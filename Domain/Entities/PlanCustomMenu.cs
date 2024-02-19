using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PlanCustomMenu
    {
        public string Name { get; set; } = null!;
        public DateTime ForDay { get; set; }
        public Guid PlanId { get; set; }
        public Guid MenuId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual Plan Plan { get; set; } = null!;
    }
}
