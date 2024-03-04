using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class MenuRepository : Repository<Menu> ,IMenuRepository
    {
        public MenuRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
