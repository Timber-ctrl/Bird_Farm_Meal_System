using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class ManagerService : BaseService, IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _managerRepository = unitOfWork.Manager;
        }

        private async Task<Manager> GetManager(Guid id)
        {
            try
            {
                var manager = await _managerRepository.Where(ma => ma.Id.Equals(id))
                    .FirstOrDefaultAsync();
                return manager != null ? manager : null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetManagerInformation(Guid id)
        {
            try
            {
                var manager = await _managerRepository.Where(ma => ma.Id.Equals(id))
                    .ProjectTo<ManagerViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return manager != null ? manager.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateManager(ManagerRegistrationModel model)
        {
            try
            {
                // Return 409 if email conflict
                if (IsEmailEximas(model.Email))
                {
                    return AppErrors.DUPLICATE_EMAIL.Conflict();
                }

                // Return 409 if phone number conflict
                if (model.Phone != null && IsPhoneNumberEximas(model.Phone))
                {
                    return AppErrors.DUPLICATE_PHONE.Conflict();
                }

                // Create new manager
                var manager = _mapper.Map<Manager>(model);
                _managerRepository.Add(manager);
                await _unitOfWork.SaveChangesAsync();

                // Return created manager
                var createdManager = await GetManager(manager.Id);
                return _mapper.Map<ManagerViewModel>(createdManager).Created();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsEmailEximas(string email)
        {
            return _managerRepository.Any(ma => ma.Email.Equals(email));
        }

        private bool IsPhoneNumberEximas(string phone)
        {
            return _managerRepository.Any(ma => ma.Phone != null && ma.Phone.Equals(phone));
        }
    }
}
