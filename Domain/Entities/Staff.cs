using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Staff
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public Guid FarmId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Farm Farm { get; set; } = null!;
        public virtual AssignStaff? AssignStaff { get; set; }
        public virtual TaskItem? TaskItem { get; set; }
    }
}
