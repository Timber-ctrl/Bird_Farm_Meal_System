using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class CareModeRepository : Repository<CareMode>, ICareModeRepository
    {
        public CareModeRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
