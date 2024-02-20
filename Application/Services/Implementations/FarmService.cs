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
    public class FarmService : BaseService, IFarmService
    {
        private readonly IFarmRepository _farmRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public FarmService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _farmRepository = unitOfWork.Farm;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetFarms(FarmFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _farmRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var farms = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<FarmViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return farms.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFarm(Guid id)
        {
            try
            {
                var farm = await _farmRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FarmViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return farm != null ? farm.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedFarm(Guid id)
        {
            try
            {
                var farm = await _farmRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FarmViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return farm != null ? farm.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateFarm(FarmCreateModel model)
        {
            try
            {
                var farm = _mapper.Map<Farm>(model);
                if (model.Thumbnail != null)
                {
                    farm.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _farmRepository.Add(farm);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedFarm(farm.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateFarm(Guid id, FarmUpdateModel model)
        {
            try
            {
                var farm = await _farmRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (farm == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Thumbnail != null)
                {
                    farm.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _mapper.Map(model, farm);
                _farmRepository.Update(farm);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetFarm(farm.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
