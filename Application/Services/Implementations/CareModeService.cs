using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class CareModeService : BaseService, ICareModeService
    {
        private readonly ICareModeRepository _careModeRepository;
        public CareModeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _careModeRepository = unitOfWork.CareMode;
        }

        public async Task<IActionResult> GetCareModes(CareModeFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _careModeRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var careModes = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<CareModeViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return careModes.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetCareMode(Guid id)
        {
            try
            {
                var careMode = await _careModeRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<CareModeViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return careMode != null ? careMode.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedCareMode(Guid id)
        {
            try
            {
                var careMode = await _careModeRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<CareModeViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return careMode != null ? careMode.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateCareMode(CareModeCreateModel model)
        {
            try
            {
                var careMode = _mapper.Map<CareMode>(model);
                _careModeRepository.Add(careMode);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedCareMode(careMode.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateCareMode(Guid id, CareModeUpdateModel model)
        {
            try
            {
                var careMode = await _careModeRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (careMode == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, careMode);
                _careModeRepository.Update(careMode);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCareMode(careMode.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
