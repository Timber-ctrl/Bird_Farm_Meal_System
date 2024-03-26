using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IMealItemService
    {
        Task<IActionResult> GetMealItems(MealItemFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetMealItem(Guid id);
        Task<IActionResult> CreateMealItem(MealItemCreateModel model);
        Task<IActionResult> UpdateMealItem(Guid id, MealItemUpdateModel model);
        Task<IActionResult> DeleteMealItem(Guid id);
    }
}
