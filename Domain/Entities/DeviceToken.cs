using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class DeviceToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public Guid? StaffId { get; set; }
        public Guid? AdminId { get; set; }
        public Guid? ManagerId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Manager? Manager { get; set; }
        public virtual Staff? Staff { get; set; }
    }
}
