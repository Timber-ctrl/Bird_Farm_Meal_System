using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IMenuMealService
    {
        Task<IActionResult> GetMenuMeals(MenuMealFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetMenuMeal(Guid id);
        Task<IActionResult> CreateMenuMeal(MenuMealCreateModel model);
        Task<IActionResult> UpdateMenuMeal(Guid id, MenuMealUpdateModel model);
        Task<IActionResult> DeleteMenuMeal(Guid id);
    }
}
