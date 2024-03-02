using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class MenuMealRepository : Repository<MenuMeal>, IMenuMealRepository
    {
        public MenuMealRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
