using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class FoodReportRepository : Repository<FoodReport>, IFoodReportRepository
    {
        public FoodReportRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}