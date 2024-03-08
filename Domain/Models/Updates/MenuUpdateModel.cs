using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Updates
{
    public class MenuUpdateModel
    {
        public string? Name { get; set; } = null!;
        public Guid? SpeciesId { get; set; }
        public Guid? CareModeId { get; set; }
    }
}
