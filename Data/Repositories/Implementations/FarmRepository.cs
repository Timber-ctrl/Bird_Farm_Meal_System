using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class FarmRepository : Repository<Farm>, IFarmRepository
    {
        public FarmRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
