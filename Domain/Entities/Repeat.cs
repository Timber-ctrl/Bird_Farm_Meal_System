using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Repeat
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public int Time { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? TaskSampleId { get; set; }

        public virtual Task? Task { get; set; }
        public virtual TaskSample? TaskSample { get; set; }
    }
}
