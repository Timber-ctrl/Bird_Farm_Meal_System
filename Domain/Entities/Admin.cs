using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Admin
    {
        public Admin()
        {
            DeviceTokens = new HashSet<DeviceToken>();
            Notifications = new HashSet<Notification>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<DeviceToken> DeviceTokens { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
