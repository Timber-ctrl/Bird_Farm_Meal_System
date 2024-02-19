using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Area
    {
        public Area()
        {
            Cages = new HashSet<Cage>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public Guid FarmId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Farm Farm { get; set; } = null!;
        public virtual ICollection<Cage> Cages { get; set; }
    }
}
