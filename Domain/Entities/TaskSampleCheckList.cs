using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskSampleCheckList
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid TaskSampleId { get; set; }
        public int Order { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual TaskSample TaskSample { get; set; } = null!;
    }
}
