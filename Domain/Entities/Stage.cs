using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Stage
    {
        public Stage()
        {
            Birds = new HashSet<Bird>();
            Cages = new HashSet<Cage>();
            Menus = new HashSet<Menu>();
            TaskSamples = new HashSet<TaskSample>();
        }

        public Guid Id { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<Cage> Cages { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<TaskSample> TaskSamples { get; set; }
    }
}
