﻿namespace Domain.Models.Creates
{
    public class MenuCreateModel
    {
        public string Name { get; set; } = null!;
        public Guid SpeciesId { get; set; }
        public Guid CareModeId { get; set; }
    }
}
