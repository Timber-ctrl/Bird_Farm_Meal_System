using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PlanDetail
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public virtual Plan Plan { get; set; } = null!;
    }
}
