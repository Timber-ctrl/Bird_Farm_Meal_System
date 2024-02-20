using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IFarmService
    {
        Task<IActionResult> GetFarms(FarmFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetFarm(Guid id);
        Task<IActionResult> CreateFarm(FarmCreateModel model);
        Task<IActionResult> UpdateFarm(Guid id, FarmUpdateModel model);
    }
}
