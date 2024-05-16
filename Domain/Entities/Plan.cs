using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Plan
    {
        public Plan()
        {
            PlanDetails = new HashSet<PlanDetail>();
        }

        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid? MenuId { get; set; }
        public Guid CageId { get; set; }
        public DateTime CreateAt { get; set; }
        public string Title { get; set; } = null!;

        public virtual Cage Cage { get; set; } = null!;
        public virtual Menu? Menu { get; set; }
        public virtual ICollection<PlanDetail> PlanDetails { get; set; }
    }
}
