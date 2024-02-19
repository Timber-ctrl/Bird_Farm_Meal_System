using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskCheckListReport
    {
        public TaskCheckListReport()
        {
            TaskCheckListReportItems = new HashSet<TaskCheckListReportItem>();
        }

        public Guid Id { get; set; }
        public Guid TaskCheckListId { get; set; }
        public DateTime FinishAt { get; set; }

        public virtual TaskCheckList TaskCheckList { get; set; } = null!;
        public virtual ICollection<TaskCheckListReportItem> TaskCheckListReportItems { get; set; }
    }
}
