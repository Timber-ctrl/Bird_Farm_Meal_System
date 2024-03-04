using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class BirdCategory
    {
        public BirdCategory()
        {
            Birds = new HashSet<Bird>();
            Species = new HashSet<Species>();
        }

        public Guid Id { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<Species> Species { get; set; }
    }
}
