using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Manager
    {
        public Manager()
        {
            DeviceTokens = new HashSet<DeviceToken>();
            Notifications = new HashSet<Notification>();
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual Farm IdNavigation { get; set; } = null!;
        public virtual Farm? Farm { get; set; }
        public virtual ICollection<DeviceToken> DeviceTokens { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
