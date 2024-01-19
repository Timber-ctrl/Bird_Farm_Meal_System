using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid StaffId { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }

        public virtual Staff Staff { get; set; } = null!;
    }
}
