using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Updates
{
    public class MenuMealUpdateModel
    {
        public Guid? MenuId { get; set; }
        public string? Name { get; set; } = null!;
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
    }
}
