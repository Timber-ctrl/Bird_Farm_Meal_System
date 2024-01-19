using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class AssignStaff
    {
        public Guid TaskId { get; set; }
        public Guid StaffId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Staff Staff { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
    }
}
