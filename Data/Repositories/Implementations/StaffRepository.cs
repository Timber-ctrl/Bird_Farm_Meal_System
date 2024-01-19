using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        public StaffRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
