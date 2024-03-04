using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class MenuMealSampleRepository : Repository<MenuMealSample> , IMenuMealSampleRepository
    {
        public MenuMealSampleRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
