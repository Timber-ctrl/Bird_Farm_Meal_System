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
    public class MenuMealService : BaseService , IMenuMealService
    {
        private readonly IMenuMealRepository _menuMealRepository;
        public MenuMealService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _menuMealRepository = unitOfWork.MenuMeal;
        }
        public async Task<IActionResult> GetMenuMeals(MenuMealFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _menuMealRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var menuMeals = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<MenuMealViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return menuMeals.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetMenuMeal(Guid id)
        {
            try
            {
                var menuMeal = await _menuMealRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuMealViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menuMeal != null ? menuMeal.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedMenuMeal(Guid id)
        {
            try
            {
                var menuMeal = await _menuMealRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuMealViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menuMeal != null ? menuMeal.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMenuMeal(MenuMealCreateModel model)
        {
            try
            {
                var menuMeal = _mapper.Map<MenuMeal>(model);
                _menuMealRepository.Add(menuMeal);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedMenuMeal(menuMeal.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateMenuMeal(Guid id, MenuMealUpdateModel model)
        {
            try
            {
                var menuMeal = await _menuMealRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (menuMeal == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, menuMeal);
                _menuMealRepository.Update(menuMeal);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetMenuMeal(menuMeal.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> DeleteMenuMeal(Guid id)
        {
            try
            {
                if (!IsMenuMealExist(id))
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                var menuMeal = await _menuMealRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                _menuMealRepository.Remove(menuMeal);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? new NoContentResult() : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsMenuMealExist(Guid id)
        {
            try
            {
                return _menuMealRepository.Any(mi => mi.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
