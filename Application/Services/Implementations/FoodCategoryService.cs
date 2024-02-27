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
    public class FoodCategoryService : BaseService, IFoodCategoryService
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        public FoodCategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _foodCategoryRepository = unitOfWork.FoodCategory;
        }

        public async Task<IActionResult> GetFoodCategories(FoodCategoryFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _foodCategoryRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var foodCategories = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<FoodCategoryViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return foodCategories.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFoodCategory(Guid id)
        {
            try
            {
                var foodCategory = await _foodCategoryRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FoodCategoryViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return foodCategory != null ? foodCategory.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedFoodCategory(Guid id)
        {
            try
            {
                var foodCategory = await _foodCategoryRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FoodCategoryViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return foodCategory != null ? foodCategory.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateFoodCategory(FoodCategoryCreateModel model)
        {
            try
            {
                var foodCategory = _mapper.Map<FoodCategory>(model);
                _foodCategoryRepository.Add(foodCategory);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedFoodCategory(foodCategory.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateFoodCategory(Guid id, FoodCategoryUpdateModel model)
        {
            try
            {
                var foodCategory = await _foodCategoryRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (foodCategory == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, foodCategory);
                _foodCategoryRepository.Update(foodCategory);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetFoodCategory(foodCategory.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
