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
    public class ManagerService : BaseService, IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _managerRepository = unitOfWork.Manager;
        }

        public async Task<IActionResult> GetManagerInformation(Guid id)
        {
            try
            {
                var manager = await _managerRepository.Where(st => st.Id.Equals(id))
                    .ProjectTo<ManagerViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                return manager != null ? manager.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
