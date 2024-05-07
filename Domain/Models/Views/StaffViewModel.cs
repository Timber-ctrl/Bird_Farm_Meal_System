﻿namespace Domain.Models.Views
{
    public class StaffViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public FarmViewModel? Farm { get; set; }
    }
}
