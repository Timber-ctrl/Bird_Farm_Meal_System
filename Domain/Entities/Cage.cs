using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Cage
    {
        public Cage()
        {
            Birds = new HashSet<Bird>();
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string? Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public string? ThumbnailUrl { get; set; }
        public Guid StageId { get; set; }
        public Guid SpeciesId { get; set; }
        public Guid FarmId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Farm Farm { get; set; } = null!;
        public virtual Species Species { get; set; } = null!;
        public virtual Stage Stage { get; set; } = null!;
        public virtual ICollection<Bird> Birds { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
