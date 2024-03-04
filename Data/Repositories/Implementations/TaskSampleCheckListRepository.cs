using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TaskSampleCheckListRepository : Repository<TaskSampleCheckList>, ITaskSampleCheckListRepository
    {
        public TaskSampleCheckListRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
