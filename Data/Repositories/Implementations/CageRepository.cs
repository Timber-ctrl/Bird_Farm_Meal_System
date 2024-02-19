using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class CageRepository : Repository<Cage>, ICageRepository
    {
        public CageRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
