using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TaskCheckListRepository : Repository<TaskCheckList>, ITaskCheckListRepository
    {
        public TaskCheckListRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
