using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TaskCheckListReportItemRepository : Repository<TaskCheckListReportItem>, ITaskCheckListReportItemRepository
    {
        public TaskCheckListReportItemRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
