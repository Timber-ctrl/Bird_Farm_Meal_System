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
        public string Title { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public MenuViewModel Menu { get; set; } = null!;
        public CageViewModel Cage { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}
