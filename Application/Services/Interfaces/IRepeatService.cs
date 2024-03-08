using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IRepeatService
    {
        Task<IActionResult> GetRepeats(RepeatFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetRepeat(Guid id);
        Task<IActionResult> CreateRepeat(RepeatCreateModel model);
        Task<IActionResult> UpdateRepeat(Guid id, RepeatUpdateModel model);
    }
}
