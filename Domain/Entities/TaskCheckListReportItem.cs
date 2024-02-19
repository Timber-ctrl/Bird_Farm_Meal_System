using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskCheckListReportItem
    {
        public Guid Id { get; set; }
        public Guid TaskCheckListReportId { get; set; }
        public string Issue { get; set; } = null!;
        public bool Positive { get; set; }
        public int Severity { get; set; }
        public string? Message { get; set; }

        public virtual TaskCheckListReport TaskCheckListReport { get; set; } = null!;
    }
}
