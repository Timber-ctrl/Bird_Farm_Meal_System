using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ITaskCheckListReportService
    {

        Task<IActionResult> GetTaskCheckListReports(TaskCheckListReportFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTaskCheckListReport(Guid id);
        Task<IActionResult> CreateTaskCheckListReport(TaskCheckListReportCreateModel model);
        Task<IActionResult> UpdateTaskCheckListReport(Guid id, TaskCheckListReportUpdateModel model);
    }
}
