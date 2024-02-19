using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ICageService
    {
        Task<IActionResult> GetCages(CageFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetCage(Guid id);
        Task<IActionResult> CreateCage(CageCreateModel model);
        Task<IActionResult> UpdateCage(Guid id, CageUpdateModel model);
    }
}
