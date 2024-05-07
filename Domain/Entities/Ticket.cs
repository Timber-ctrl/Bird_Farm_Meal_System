using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Ticket
    {
        public Guid Id { get; set; }
        public string TicketCategory { get; set; } = null!;
        public Guid CreatorId { get; set; }
        public string Title { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public Guid? AssigneeId { get; set; }
        public Guid? CageId { get; set; }
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public string? ResultImage { get; set; }
        public string? ResultDescription { get; set; }

        public virtual Staff? Assignee { get; set; }
        public virtual Cage? Cage { get; set; }
        public virtual Staff Creator { get; set; } = null!;
    }
}
