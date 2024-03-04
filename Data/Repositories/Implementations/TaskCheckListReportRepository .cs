using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class TaskCheckListReportRepository : Repository<TaskCheckListReport>, ITaskCheckListReportRepository
    {
        public TaskCheckListReportRepository(BirdFarmMealSystemContext context) : base(context)
        {
        }
    }
}
