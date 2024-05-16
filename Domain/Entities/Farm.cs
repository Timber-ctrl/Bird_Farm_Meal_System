using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Farm
    {
        public Farm()
        {
            Areas = new HashSet<Area>();
            Staff = new HashSet<Staff>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Guid ManagerId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Manager Manager { get; set; } = null!;
        public virtual Manager? ManagerNavigation { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
