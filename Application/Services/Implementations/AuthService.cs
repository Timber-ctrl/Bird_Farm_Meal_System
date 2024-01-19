using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Models.Authentications;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IManagerRepository _managerRepository;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _staffRepository = unitOfWork.Staff;
            _managerRepository = unitOfWork.Manager;
        }

        public async Task<AuthModel> GetUser(Guid id)
        {
            try
            {
                // Find staff in staff table
                if (_staffRepository.Any(st => st.Id.Equals(id)))
                {
                    var staff = await _staffRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    staff!.Role = UserRoles.Staff;
                    return staff;
                }

                // Find manager in manager table
                if (_managerRepository.Any(st => st.Id.Equals(id)))
                {
                    var manager = await _managerRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    manager!.Role = UserRoles.Manager;
                    return manager;
                }

                // Return null if not found any user
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
