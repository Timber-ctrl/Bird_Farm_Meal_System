using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IBirdService
    {
        Task<IActionResult> GetBirds(BirdFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetBird(Guid id);
        Task<IActionResult> CreateBird(BirdCreateModel model);
        Task<IActionResult> UpdateBird(Guid id, BirdUpdateModel model);
    }
}
