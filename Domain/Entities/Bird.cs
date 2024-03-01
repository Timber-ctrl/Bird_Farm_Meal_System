﻿namespace Domain.Entities
{
    public partial class Bird
    {
        public Guid Id { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Characteristic { get; set; }
        public string Name { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string? Code { get; set; }
        public Guid CageId { get; set; }
        public Guid SpeciesId { get; set; }
        public Guid CareModeId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Cage Cage { get; set; } = null!;
        public virtual CareMode CareMode { get; set; } = null!;
        public virtual Species Species { get; set; } = null!;
    }
}
