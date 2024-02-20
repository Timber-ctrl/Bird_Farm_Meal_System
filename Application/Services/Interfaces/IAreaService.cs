using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IAreaService
    {
        Task<IActionResult> GetAreas(AreaFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetArea(Guid id);
        Task<IActionResult> CreateArea(AreaCreateModel model);
        Task<IActionResult> UpdateArea(Guid id, AreaUpdateModel model);
    }
}
