using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        public FoodRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
