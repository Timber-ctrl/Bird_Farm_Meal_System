using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MenuSammple
    {
        public MenuSammple()
        {
            MenuMealSamples = new HashSet<MenuMealSample>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid SpeciesId { get; set; }
        public Guid CareModeId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual CareMode CareMode { get; set; } = null!;
        public virtual Species Species { get; set; } = null!;
        public virtual ICollection<MenuMealSample> MenuMealSamples { get; set; }
    }
}
