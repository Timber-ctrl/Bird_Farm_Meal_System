using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class MealItemRepository : Repository<MealItem>, IMealItemRepository
    {
        public MealItemRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
