using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IMenuMealSampleService
    {
        Task<IActionResult> GetMenuMealSamples(MenuMealSampleFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetMenuMealSample(Guid id);
        Task<IActionResult> CreateMenuMealSample(MenuMealSampleCreateModel model);
        Task<IActionResult> UpdateMenuMealSample(Guid id, MenuMealSampleUpdateModel model);
        Task<IActionResult> DeleteMenuMealSample(Guid id);
    }
}
