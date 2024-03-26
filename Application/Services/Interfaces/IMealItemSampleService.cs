using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IMealItemSampleService
    {
        Task<IActionResult> GetMealItemSamples(MealItemSampleFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetMealItemSample(Guid id);
        Task<IActionResult> CreateMealItemSample(MealItemSampleCreateModel model);
        Task<IActionResult> UpdateMealItemSample(Guid id, MealItemSampleUpdateModel model);
        Task<IActionResult> DeleteMealItemSample(Guid id);
    }
}
