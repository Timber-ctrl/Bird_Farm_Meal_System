using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class StaffService : BaseService, IStaffService
    {
        private readonly IStaffRepository _StaffRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public StaffService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _StaffRepository = unitOfWork.Staff;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetStaffs(StaffFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _StaffRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                if (filter.Status != null)
                {
                    query = query.Where(cg => cg.Status.Equals(filter.Status));
                }
                var totalRows = query.Count();
                var species = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<StaffViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return species.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
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

        public async Task<IActionResult> UpdateStaff(Guid id, StaffUpdateModel model)
        {
            try
            {
                var Staff = await _StaffRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (Staff == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                // Return 409 if phone number conflict
                if (model.Phone != null && IsPhoneNumberExists(model.Phone))
                {
                    return AppErrors.DUPLICATE_PHONE.Conflict();
                }
                if (model.Avatar != null)
                {
                    Staff.AvatarUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Avatar);
                }
                _mapper.Map(model, Staff);
                _StaffRepository.Update(Staff);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetStaffInformation(Staff.Id) : AppErrors.UPDATE_FAILED.BadRequest();
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
