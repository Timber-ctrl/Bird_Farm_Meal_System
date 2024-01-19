using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UnitOfMeasurement
    {
        public UnitOfMeasurement()
        {
            Foods = new HashSet<Food>();
            MenuItems = new HashSet<MenuItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
