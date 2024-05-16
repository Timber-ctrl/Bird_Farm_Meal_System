using Domain.Entities;

namespace Domain.Models.Views
{
    public class FarmViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public ManagerViewModel Manager { get; set; } = null!;
        public ICollection<AreaViewModel> Areas { get; set; } = new List<AreaViewModel>();
        public DateTime CreateAt { get; set; }
    }
}
