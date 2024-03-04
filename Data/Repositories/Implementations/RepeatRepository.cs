using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class RepeatRepository : Repository<Repeat> , IRepeatRepository
    {
        public RepeatRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
