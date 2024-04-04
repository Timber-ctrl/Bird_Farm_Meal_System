using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Creates
{
    public class PlanCreateModel
    {
        public string Title { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid MenuId { get; set; }
        public Guid CageId { get; set; }
    }
}
