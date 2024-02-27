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
    public class BirdCategoryService : BaseService, IBirdCategoryService
    {
        private readonly IBirdCategoryRepository _birdCategoryRepository;
        public BirdCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _birdCategoryRepository = unitOfWork.BirdCategory;
        }

        public async Task<IActionResult> GetBirdCategories(BirdCategoryFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _birdCategoryRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var birdCategories = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<BirdCategoryViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return birdCategories.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetBirdCategory(Guid id)
        {
            try
            {
                var birdCategory = await _birdCategoryRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<BirdCategoryViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return birdCategory != null ? birdCategory.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedBirdCategory(Guid id)
        {
            try
            {
                var birdCategory = await _birdCategoryRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<BirdCategoryViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return birdCategory != null ? birdCategory.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateBirdCategory(BirdCategoryCreateModel model)
        {
            try
            {
                var birdCategory = _mapper.Map<BirdCategory>(model);
                _birdCategoryRepository.Add(birdCategory);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedBirdCategory(birdCategory.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateBirdCategory(Guid id, BirdCategoryUpdateModel model)
        {
            try
            {
                var birdCategory = await _birdCategoryRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (birdCategory == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, birdCategory);
                _birdCategoryRepository.Update(birdCategory);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetBirdCategory(birdCategory.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
