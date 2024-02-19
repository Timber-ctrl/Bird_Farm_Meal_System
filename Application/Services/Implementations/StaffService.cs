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
    public class staffService : BaseService, IStaffService
    {
        private readonly IStaffRepository _StaffRepository;
        public staffService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _StaffRepository = unitOfWork.Staff;
        }

        private async Task<Staff> GetStaff(Guid id)
        {
            try
            {
                var Staff = await _StaffRepository.Where(st => st.Id.Equals(id))
                    .FirstOrDefaultAsync();
                return Staff != null ? Staff : null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetStaffInformation(Guid id)
        {
            try
            {
                var Staff = await _StaffRepository.Where(st => st.Id.Equals(id))
                    .ProjectTo<StaffViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return Staff != null ? Staff.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateStaff(StaffRegistrationModel model)
        {
            try
            {
                // Return 409 if email conflict
                if (IsEmailExists(model.Email))
                {
                    return AppErrors.DUPLICATE_EMAIL.Conflict();
                }

                // Return 409 if phone number conflict
                if (model.Phone != null && IsPhoneNumberExists(model.Phone))
                {
                    return AppErrors.DUPLICATE_PHONE.Conflict();
                }

                // Create new Staff
                var Staff = _mapper.Map<Staff>(model);
                _StaffRepository.Add(Staff);
                await _unitOfWork.SaveChangesAsync();

                // Return created Staff
                var createdStaff = await GetStaff(Staff.Id);
                return _mapper.Map<StaffViewModel>(createdStaff).Created();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsEmailExists(string email)
        {
            return _StaffRepository.Any(st => st.Email.Equals(email));
        }

        private bool IsPhoneNumberExists(string phone)
        {
            return _StaffRepository.Any(st => st.Phone != null && st.Phone.Equals(phone));
        }
    }
}
