using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class FoodMealPlanViewModel
    {
        public FoodViewModel Food {  get; set; }
        public double PlanQuantity { get; set; }
    }
}
