namespace Domain.Models.Views
{
    public class PlanViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public MenuViewModel Menu { get; set; } = null!;
        public CageViewModel Cage { get; set; } = null!;
        public ICollection<PlanDetailViewModel> PlanDetails { get; set; } = new List<PlanDetailViewModel>();
        public DateTime CreateAt { get; set; }
    }
}
