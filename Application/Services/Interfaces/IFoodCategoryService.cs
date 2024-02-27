using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IFoodCategoryService
    {
        Task<IActionResult> GetFoodCategories(FoodCategoryFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetFoodCategory(Guid id);
        Task<IActionResult> CreateFoodCategory(FoodCategoryCreateModel model);
        Task<IActionResult> UpdateFoodCategory(Guid id, FoodCategoryUpdateModel model);
    }
}
