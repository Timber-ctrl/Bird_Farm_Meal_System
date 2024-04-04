using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Cage
    {
        public Cage()
        {
            Birds = new HashSet<Bird>();
            Plans = new HashSet<Plan>();
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string? Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public string? ThumbnailUrl { get; set; }
        public Guid AreaId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Area Area { get; set; } = null!;
        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
