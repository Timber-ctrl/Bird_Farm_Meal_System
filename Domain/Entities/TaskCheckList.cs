using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TaskCheckList
    {
        public TaskCheckList()
        {
            TaskCheckListReports = new HashSet<TaskCheckListReport>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public Guid TaskId { get; set; }
        public Guid? AsigneeId { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Staff? Asignee { get; set; }
        public virtual Task Task { get; set; } = null!;
        public virtual ICollection<TaskCheckListReport> TaskCheckListReports { get; set; }
    }
}
