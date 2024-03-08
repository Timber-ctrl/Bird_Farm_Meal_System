using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Implementations
{
    public interface ITaskSampleCheckListService
    {
        Task<IActionResult> GetTaskSampleCheckLists(TaskSampleCheckListFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTaskSampleCheckList(Guid id);
        Task<IActionResult> CreateTaskSampleCheckList(TaskSampleCheckListCreateModel model);
        Task<IActionResult> UpdateTaskSampleCheckList(Guid id, TaskSampleCheckListUpdateModel model);
    }
}
