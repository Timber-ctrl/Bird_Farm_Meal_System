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
    public class StaffService : BaseService, IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _staffRepository = unitOfWork.Staff;
        }

        private async Task<Staff> GetStaff(Guid id)
        {
            try
            {
                var staff = await _staffRepository.Where(st => st.Id.Equals(id))
                    .FirstOrDefaultAsync();
                return staff != null ? staff : null!;
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
                var staff = await _staffRepository.Where(st => st.Id.Equals(id))
                    .ProjectTo<StaffViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return staff != null ? staff.Ok() : AppErrors.NOT_FOUND.NotFound();
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

                // Create new staff
                var staff = _mapper.Map<Staff>(model);
                _staffRepository.Add(staff);
                await _unitOfWork.SaveChangesAsync();

                // Return created staff
                var createdStaff = await GetStaff(staff.Id);
                return _mapper.Map<StaffViewModel>(createdStaff).Created();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsEmailExists(string email)
        {
            return _staffRepository.Any(st => st.Email.Equals(email));
        }

        private bool IsPhoneNumberExists(string phone)
        {
            return _staffRepository.Any(st => st.Phone != null && st.Phone.Equals(phone));
        }
    }
}
