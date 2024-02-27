using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IFoodService
    {
        Task<IActionResult> GetFoods(FoodFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetFood(Guid id);
        Task<IActionResult> CreateFood(FoodCreateModel model);
        Task<IActionResult> UpdateFood(Guid id, FoodUpdateModel model);
    }
}
