using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ICareModeService
    {
        Task<IActionResult> GetCareModes(CareModeFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetCareMode(Guid id);
        Task<IActionResult> CreateCareMode(CareModeCreateModel model);
        Task<IActionResult> UpdateCareMode(Guid id, CareModeUpdateModel model);
    }
}
