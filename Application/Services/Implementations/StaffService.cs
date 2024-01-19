using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
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
    }
}
