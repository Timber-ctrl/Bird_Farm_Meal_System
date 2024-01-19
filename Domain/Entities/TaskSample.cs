using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskSample
    {
        public TaskSample()
        {
            TaskSampleItems = new HashSet<TaskSampleItem>();
        }

        public Guid Id { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid StageId { get; set; }

        public virtual Stage Stage { get; set; } = null!;
        public virtual Repeat? Repeat { get; set; }
        public virtual ICollection<TaskSampleItem> TaskSampleItems { get; set; }
    }
}
