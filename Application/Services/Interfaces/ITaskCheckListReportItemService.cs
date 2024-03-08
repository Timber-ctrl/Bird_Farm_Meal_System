using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ITaskCheckListReportItemService
    {
        Task<IActionResult> GetTaskCheckListReportItems(TaskCheckListReportItemFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTaskCheckListReportItem(Guid id);
        Task<IActionResult> CreateTaskCheckListReportItem(TaskCheckListReportItemCreateModel model);
        Task<IActionResult> UpdateTaskCheckListReportItem(Guid id, TaskCheckListReportItemUpdateModel model);
    }
}
