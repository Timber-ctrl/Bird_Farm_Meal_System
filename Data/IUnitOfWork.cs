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
        public IUnitOfMeasurementRepository UnitOfMeasurement { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
