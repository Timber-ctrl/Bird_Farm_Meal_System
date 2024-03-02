using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TaskSampleRepository : Repository<TaskSample> , ITaskSampleRepository
    {
        public TaskSampleRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
