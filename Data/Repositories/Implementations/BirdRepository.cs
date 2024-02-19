using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class BirdRepository : Repository<Bird>, IBirdRepository
    {
        public BirdRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
