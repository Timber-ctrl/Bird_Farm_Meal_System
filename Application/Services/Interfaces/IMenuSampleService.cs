using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IMenuSampleService
    {
        Task<IActionResult> GetMenuSamples(MenuSampleFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetMenuSample(Guid id);
        Task<IActionResult> CreateMenuSample(MenuSampleCreateModel model);
        Task<IActionResult> UpdateMenuSample(Guid id, MenuSampleUpdateModel model);
    }
}
