using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Species
    {
        public Species()
        {
            Birds = new HashSet<Bird>();
            Cages = new HashSet<Cage>();
            Menus = new HashSet<Menu>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
