using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TaskRepository : Repository<Domain.Entities.Task> , ITaskRepository
    {
        public TaskRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
