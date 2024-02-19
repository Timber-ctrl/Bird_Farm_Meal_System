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

        public IStaffRepository _Staff = null!;
        public IManagerRepository _manager = null!;
        public ICageRepository _cage = null!;
        public IBirdRepository _bird = null!;

        public IStaffRepository Staff
        {
            get { return _Staff ??= new StaffRepository(_context); }
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
