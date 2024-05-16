using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class PlanDetailRepository : Repository<PlanDetail>, IPlanDetailRepository
    {
        public PlanDetailRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
