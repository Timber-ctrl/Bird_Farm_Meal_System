using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Updates
{
    public class TicketUpdateModel
    {
        public string? TicketCategory { get; set; } = null!;
        public Guid? CreatorId { get; set; }
        public string? Priority { get; set; } = null!;
        public Guid? AssigneeId { get; set; }
        public Guid? CageId { get; set; }
        public string? Description { get; set; } = null!;
        public IFormFile? Image { get; set; } = null!;
        public string? Status { get; set; } = null!;
    }
}
