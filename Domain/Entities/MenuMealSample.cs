using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class MenuMealSample
    {
        public MenuMealSample()
        {
            MealItemSamples = new HashSet<MealItemSample>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid MenuSampleId { get; set; }

        public virtual MenuSammple MenuSample { get; set; } = null!;
        public virtual ICollection<MealItemSample> MealItemSamples { get; set; }
    }
}
