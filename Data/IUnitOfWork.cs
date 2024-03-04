using Data.Repositories.Interfaces;

namespace Data
{
    public interface IUnitOfWork
    {
        public IStaffRepository Staff { get; }
        public IManagerRepository Manager { get; }
        public ICageRepository Cage { get; }
        public IBirdRepository Bird { get; }
        public IFarmRepository Farm { get; }
        public IAreaRepository Area { get; }
        public ICareModeRepository CareMode { get; }
        public ISpeciesRepository Species { get; }
        public IFoodRepository Food { get; }
        public IFoodCategoryRepository FoodCategory { get; }
        public IBirdCategoryRepository BirdCategory { get; }
        public IAssignStaffRepository AssignStaff { get; }
        public IUnitOfMeasurementRepository UnitOfMeasurement { get; }
        //get Admin Repository
        public IAdminRepository Admin { get; }
        //get MealItem Repository
        public IMealItemRepository MealItem { get; }
        //get MealItemSample Repository
        public IMealItemSampleRepository MealItemSample { get; }
        //get Menu Repository
        public IMenuRepository Menu { get; }
        //get MenuMeal Repository
        public IMenuMealRepository MenuMeal { get; }
        //get MenuMealSample Repository
        public IMenuMealSampleRepository MenuMealSample { get; }
        //get MenuSample Repository
        public IMenuSampleRepository MenuSample { get; }
        //get plan Repository
        public IPlanRepository Plan { get; }
        //get repeat Repository
        public IRepeatRepository Repeat { get; }
        //get Task Repository
        public ITaskRepository Task { get; }
        //get TaskCheckList Repository
        public ITaskCheckListRepository TaskCheckList { get; }
        //get TaskCheckListReport Repository
        public ITaskCheckListReportRepository TaskCheckListReport { get; }
        //get TaskCheckListReportItem Repository
        public ITaskCheckListReportItemRepository TaskCheckListReportItem { get; }
        //get TaskSample Repository
        public ITaskSampleRepository TaskSample { get; }
        //get TaskSampleCheckList Repository
        public ITaskSampleCheckListRepository TaskSampleCheckList { get; }
        //get Ticket Repository
        public ITicketRepository Ticket { get; }



        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
