using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Farm
    {
        public Farm()
        {
            Cages = new HashSet<Cage>();
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
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
