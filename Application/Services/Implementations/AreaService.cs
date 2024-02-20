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
    public class AreaService : BaseService, IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public AreaService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _areaRepository = unitOfWork.Area;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetAreas(AreaFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _areaRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var areas = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<AreaViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return areas.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetArea(Guid id)
        {
            try
            {
                var area = await _areaRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<AreaViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return area != null ? area.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedArea(Guid id)
        {
            try
            {
                var area = await _areaRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<AreaViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return area != null ? area.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateArea(AreaCreateModel model)
        {
            try
            {
                var area = _mapper.Map<Area>(model);
                if (model.Thumbnail != null)
                {
                    area.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _areaRepository.Add(area);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedArea(area.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateArea(Guid id, AreaUpdateModel model)
        {
            try
            {
                var area = await _areaRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (area == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Thumbnail != null)
                {
                    area.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _mapper.Map(model, area);
                _areaRepository.Update(area);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetArea(area.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
