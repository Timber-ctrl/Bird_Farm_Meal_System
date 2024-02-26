using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class BirdCategoryRepository : Repository<BirdCategory>, IBirdCategoryRepository
    {
        public BirdCategoryRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
