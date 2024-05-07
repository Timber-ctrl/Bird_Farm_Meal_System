using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BirdFarmMealSystemContext _context;
        private IDbContextTransaction _transaction = null!;

        public UnitOfWork(BirdFarmMealSystemContext context)
        {
            _context = context;
        }

        public IStaffRepository _staff = null!;
        public IManagerRepository _manager = null!;
        public ICageRepository _cage = null!;
        public IBirdRepository _bird = null!;
        public IFarmRepository _farm = null!;
        public IAreaRepository _area = null!;
        public ICareModeRepository _careMode = null!;
        public ISpeciesRepository _species = null!;
        public IFoodRepository _food = null!;
        public IFoodCategoryRepository _foodCategory = null!;
        public IBirdCategoryRepository _birdCategory = null!;
        public IAssignStaffRepository _assignStaff = null!;
        public INotificationRepository _notification = null!;
        public IDeviceTokenRepository _deviceToken = null!;
        public IUnitOfMeasurementRepository _unitOfMeasurement = null!;
        //Admin
        public IAdminRepository _admin = null!;
        //Meal
        public IMealItemRepository _mealItem = null!;
        //Meal Sample
        public IMealItemSampleRepository _mealItemSample = null!;
        //Menu
        public IMenuRepository _menu = null!;
        //Menu Meal
        public IMenuMealRepository _menuMeal = null!;
        //Menu Meal Sample
        public IMenuMealSampleRepository _menuMealSample = null!;
        //Menu Sample
        public IMenuSampleRepository _menuSample = null!;
        //Plan
        public IPlanRepository _plan = null!;
        //Repeat
        public IRepeatRepository _repeat = null!;
        //Task
        public ITaskRepository _task = null!;
        //TaskCheckList
        public ITaskCheckListRepository _taskCheckList = null!;
        //TaskCheckListReport
        public ITaskCheckListReportRepository _taskCheckListReport = null!;
        //TaskCheckListReportItem
        public ITaskCheckListReportItemRepository _taskCheckListReportItem = null!;
        //TaskSample
        public ITaskSampleRepository _taskSample = null!;
        //TaskSampleCheckList
        public ITaskSampleCheckListRepository _taskSampleCheckList = null!;
        //Ticket
        public ITicketRepository _ticket = null!;
        //FoodReport
        public IFoodReportRepository _foodReport = null!;

        public IStaffRepository Staff
        {
            get { return _staff ??= new StaffRepository(_context); }
        }

        public IManagerRepository Manager
        {
            get { return _manager ??= new ManagerRepository(_context); }
        }

        public ICageRepository Cage
        {
            get { return _cage ??= new CageRepository(_context); }
        }

        public IBirdRepository Bird
        {
            get { return _bird ??= new BirdRepository(_context); }
        }

        public IFarmRepository Farm
        {
            get { return _farm ??= new FarmRepository(_context); }
        }

        public ICareModeRepository CareMode
        {
            get { return _careMode ??= new CareModeRepository(_context); }
        }

        public IAreaRepository Area
        {
            get { return _area ??= new AreaRepository(_context); }
        }
      
        public ISpeciesRepository Species
        {
            get { return _species ??= new SpeciesRepository(_context); }
        }

        public IFoodRepository Food
        {
            get { return _food ??= new FoodRepository(_context); }
        }

        public IFoodCategoryRepository FoodCategory
        {
            get { return _foodCategory ??= new FoodCategoryRepository(_context); }
        }

        public IBirdCategoryRepository BirdCategory
        {
            get { return _birdCategory ??= new BirdCategoryRepository(_context); }
        }

        public IAssignStaffRepository AssignStaff
        {
            get { return _assignStaff ??= new AssignStaffRepository(_context); }
        }

        public INotificationRepository Notification
        {
            get { return _notification ??= new NotificationRepository(_context); }
        }

        public IDeviceTokenRepository DeviceToken
        {
            get { return _deviceToken ??= new DeviceTokenRepository(_context); }
        }

        public IUnitOfMeasurementRepository UnitOfMeasurement
        {
            get { return _unitOfMeasurement ??= new UnitOfMeasurementRepository(_context); }
        }

        //Admin
        public IAdminRepository Admin
        {
            get { return _admin ??= new AdminRepository(_context); }
        }

        //MealItem
        public IMealItemRepository MealItem
        {
            get { return _mealItem ??= new MealItemRepository(_context); }
        }

        //MealItemSample
        public IMealItemSampleRepository MealItemSample
        {
            get { return _mealItemSample ??= new MealItemSampleRepository(_context); }
        }

        //Menu
        public IMenuRepository Menu
        {
            get { return _menu ??= new MenuRepository(_context); }
        }

        //Menu Meal
        public IMenuMealRepository MenuMeal
        {
            get { return _menuMeal ??= new MenuMealRepository(_context); }
        }

        //MenuMealSample
        public IMenuMealSampleRepository MenuMealSample
        {
            get { return _menuMealSample ??= new MenuMealSampleRepository(_context); }
        }

        //MenuMealSample
        public IMenuSampleRepository MenuSample
        {
            get { return _menuSample ??= new MenuSampleRepository(_context); }
        }

        //Plan
        public IPlanRepository Plan
        {
            get { return _plan ??= new PlanRepository(_context); }
        }

        //Repeat
        public IRepeatRepository Repeat
        {
            get { return _repeat ??= new RepeatRepository(_context); }
        }

        //Task
        public ITaskRepository Task
        {
            get { return _task ??= new TaskRepository(_context); }
        }

        //TaskCheckList
        public ITaskCheckListRepository TaskCheckList
        {
            get { return _taskCheckList ??= new TaskCheckListRepository(_context); }
        }

        //TaskCheckListReport
        public ITaskCheckListReportRepository TaskCheckListReport
        {
            get { return _taskCheckListReport ??= new TaskCheckListReportRepository(_context); }
        }

        //TaskCheckListReportItem
        public ITaskCheckListReportItemRepository TaskCheckListReportItem
        {
            get { return _taskCheckListReportItem ??= new TaskCheckListReportItemRepository(_context); }
        }

        //TaskSample
        public ITaskSampleRepository TaskSample
        {
            get { return _taskSample ??= new TaskSampleRepository(_context); }
        }

        //TaskSampleCheckList
        public ITaskSampleCheckListRepository TaskSampleCheckList
        {
            get { return _taskSampleCheckList ??= new TaskSampleCheckListRepository(_context); }
        }
        //Ticket
        public ITicketRepository Ticket
        {
            get { return _ticket ??= new TicketRepository(_context); }
        }
        public IFoodReportRepository FoodReport
        {
            get { return _foodReport ??= new FoodReportRepository(_context); }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }



        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null!;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null!;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
