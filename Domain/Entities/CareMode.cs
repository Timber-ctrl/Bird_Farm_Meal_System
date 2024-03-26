using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class CareMode
    {
        public CareMode()
        {
            Birds = new HashSet<Bird>();
            MenuSammples = new HashSet<MenuSammple>();
            TaskSamples = new HashSet<TaskSample>();
        }

        public Guid Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<MenuSammple> MenuSammples { get; set; }
        public virtual ICollection<TaskSample> TaskSamples { get; set; }
    }
}
