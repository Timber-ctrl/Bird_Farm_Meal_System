using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskSample
    {
        public TaskSample()
        {
            Repeats = new HashSet<Repeat>();
            TaskSampleCheckLists = new HashSet<TaskSampleCheckList>();
        }

        public Guid Id { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CareModeId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual CareMode CareMode { get; set; } = null!;
        public virtual ICollection<Repeat> Repeats { get; set; }
        public virtual ICollection<TaskSampleCheckList> TaskSampleCheckLists { get; set; }
    }
}
