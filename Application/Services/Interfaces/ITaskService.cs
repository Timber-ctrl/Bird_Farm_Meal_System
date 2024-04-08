using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IActionResult> GetTasks(TaskFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTask(Guid id);
        Task<IActionResult> GetStaffTask(Guid id, PaginationRequestModel pagination);
        Task<IActionResult> CreateTask(TaskCreateModel model);
        Task<IActionResult> AssignStaff(AssignStaffCreateModel model);
        Task<IActionResult> DeleteAssignStaff(Guid taskId, Guid staffId);
        Task<IActionResult> UpdateTask(Guid id, TaskUpdateModel model);
    }
}