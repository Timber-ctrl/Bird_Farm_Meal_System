using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        public PlanRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
