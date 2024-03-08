using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Creates
{
    public class TaskCheckListReportItemCreateModel
    {
        public Guid TaskCheckListReportId { get; set; }
        public string Issue { get; set; } = null!;
        public bool Positive { get; set; }
        public int Severity { get; set; }
        public string? Message { get; set; }
    }
}
