using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Task
    {
        public Guid Id { get; set; }
        public Guid CageId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid ManagerId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Cage Cage { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null!;
        public virtual AssignStaff? AssignStaff { get; set; }
        public virtual Repeat? Repeat { get; set; }
    }
}
