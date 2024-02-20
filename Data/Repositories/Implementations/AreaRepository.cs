using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
