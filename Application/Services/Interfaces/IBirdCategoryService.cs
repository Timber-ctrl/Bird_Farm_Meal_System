using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IBirdCategoryService
    {
        Task<IActionResult> GetBirdCategories(BirdCategoryFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetBirdCategory(Guid id);
        Task<IActionResult> CreateBirdCategory(BirdCategoryCreateModel model);
        Task<IActionResult> UpdateBirdCategory(Guid id, BirdCategoryUpdateModel model);
    }
}
