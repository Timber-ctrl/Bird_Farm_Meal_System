using Application.Services.Interfaces;
using AutoMapper;
using Data.Repositories.Interfaces;
using Data;
using Data.Repositories.Implementations;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Updates;

namespace Application.Services.Implementations
{
    public class MealItemService : BaseService , IMealItemService
    {
        private readonly IMealItemRepository _mealItemRepository;
        public MealItemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _mealItemRepository = unitOfWork.MealItem;
        }
        public async Task<IActionResult> GetMealItems(MealItemFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _mealItemRepository.GetAll();
                if (filter.FoodId != null)
                {
                    query = query.Where(cg => cg.FoodId.Equals(filter.FoodId));
                }
                var totalRows = query.Count();
                var mealItems = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<MealItemViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return mealItems.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetMealItem(Guid id)
        {
            try
            {
                var mealItem = await _mealItemRepository.Where(cg => cg.MenuMealId.Equals(id)).AsNoTracking()
                    .ProjectTo<MealItemViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return mealItem != null ? mealItem.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedMealItem(Guid id)
        {
            try
            {
                var mealItem = await _mealItemRepository.Where(cg => cg.MenuMealId.Equals(id)).AsNoTracking()
                    .ProjectTo<MealItemViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return mealItem != null ? mealItem.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMealItem(MealItemCreateModel model)
        {
            try
            {
                var mealItem = _mapper.Map<MealItem>(model);
                
                _mealItemRepository.Add(mealItem);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedMealItem(mealItem.MenuMealId) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateMealItem(Guid id, MealItemUpdateModel model)
        {
            try
            {
                var mealItem = await _mealItemRepository.FirstOrDefaultAsync(cg => cg.MenuMealId.Equals(id));
                if (mealItem == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
               
                _mapper.Map(model, mealItem);
                _mealItemRepository.Update(mealItem);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetMealItem(mealItem.MenuMealId) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
