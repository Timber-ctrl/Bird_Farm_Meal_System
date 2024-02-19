using Data.Repositories.Interfaces;

namespace Data
{
    public interface IUnitOfWork
    {
        public IStaffRepository Staff { get; }
        public IManagerRepository Manager { get; }
        public ICageRepository Cage { get; }
        public IBirdRepository Bird { get; }
        
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
