using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Species
    {
        public Species()
        {
            Birds = new HashSet<Bird>();
            MenuSammples = new HashSet<MenuSammple>();
        }

        public Guid Id { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string Name { get; set; } = null!;
        public Guid BirdCategoryId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual BirdCategory BirdCategory { get; set; } = null!;
        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<MenuSammple> MenuSammples { get; set; }
    }
}
