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
    public class CageService : BaseService, ICageService
    {
        private readonly ICageRepository _cageRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public CageService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _cageRepository = unitOfWork.Cage;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetCages(CageFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _cageRepository.GetAll();
                if (filter.Code != null)
                {
                    query = query.Where(cg => cg.Code.Contains(filter.Code));
                }
                var totalRows = query.Count();
                var cages = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<CageViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return cages.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetCage(Guid id)
        {
            try
            {
                var cage = await _cageRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<CageViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return cage != null ? cage.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedCage(Guid id)
        {
            try
            {
                var cage = await _cageRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<CageViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return cage != null ? cage.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateCage(CageCreateModel model)
        {
            try
            {
                var cage = _mapper.Map<Cage>(model);
                if (model.Thumbnail != null)
                {
                    cage.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _cageRepository.Add(cage);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedCage(cage.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateCage(Guid id, CageUpdateModel model)
        {
            try
            {
                var cage = await _cageRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (cage == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Thumbnail != null)
                {
                    cage.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _mapper.Map(model, cage);
                _cageRepository.Update(cage);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCage(cage.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
