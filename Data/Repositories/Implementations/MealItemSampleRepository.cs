using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class MealItemSampleRepository : Repository<MealItemSample>, IMealItemSampleRepository
    {
        public MealItemSampleRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
