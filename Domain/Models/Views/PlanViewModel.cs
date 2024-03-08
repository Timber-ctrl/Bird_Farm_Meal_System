using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class PlanViewModel
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid MenuId { get; set; }
        public Guid CageId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
