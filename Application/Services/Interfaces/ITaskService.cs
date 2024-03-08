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
        Task<IActionResult> CreateTask(TaskCreateModel model);
        Task<IActionResult> UpdateTask(Guid id, TaskUpdateModel model);
    }
}