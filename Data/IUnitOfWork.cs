using Data.Repositories.Interfaces;

namespace Data
{
    public interface IUnitOfWork
    {
        public IStaffRepository Staff { get; }
        public IManagerRepository Manager { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
