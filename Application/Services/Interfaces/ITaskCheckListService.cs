using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ITaskCheckListService
    {
        Task<IActionResult> GetTaskCheckLists(TaskCheckListFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTaskCheckList(Guid id);
        Task<IActionResult> CreateTaskCheckList(TaskCheckListCreateModel model);
        Task<IActionResult> UpdateTaskCheckList(Guid id, TaskCheckListUpdateModel model);
    }
}
