using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ITaskSampleService
    {
        Task<IActionResult> GetTaskSamples(TaskSampleFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTaskSample(Guid id);
        Task<IActionResult> CreateTaskSample(TaskSampleCreateModel model);
        Task<IActionResult> UpdateTaskSample(Guid id, TaskSampleUpdateModel model);
    }
}
