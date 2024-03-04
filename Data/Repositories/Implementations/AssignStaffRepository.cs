using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class AssignStaffRepository : Repository<AssignStaff>, IAssignStaffRepository
    {
        public AssignStaffRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
