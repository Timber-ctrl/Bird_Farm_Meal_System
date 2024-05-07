using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Notification
    {
        public Guid Id { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? AdminId { get; set; }
        public Guid? ManagerId { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Type { get; set; }
        public string? Link { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Manager? Manager { get; set; }
        public virtual Staff? Staff { get; set; }
    }
}
