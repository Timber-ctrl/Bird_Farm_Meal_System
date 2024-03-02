using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class MenuSampleRepository : Repository<MenuSammple> , IMenuSampleRepository
    {
        public MenuSampleRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
