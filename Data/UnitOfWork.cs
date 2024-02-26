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
        public IUnitOfMeasurementRepository _unitOfMeasurement = null!;

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

        public IUnitOfMeasurementRepository UnitOfMeasurement
        {
            get { return _unitOfMeasurement ??= new UnitOfMeasurementRepository(_context); }
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
