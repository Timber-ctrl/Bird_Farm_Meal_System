﻿namespace Domain.Models.Filters
{
    public class BirdFilterModel
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CageId { get; set; }
    }
}
