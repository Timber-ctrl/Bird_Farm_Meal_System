using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Plan
    {
        public Plan()
        {
            PlanMenus = new HashSet<PlanMenu>();
        }

        public Guid Id { get; set; }
        public Guid BirdId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Bird Bird { get; set; } = null!;
        public virtual ICollection<PlanMenu> PlanMenus { get; set; }
    }
}
