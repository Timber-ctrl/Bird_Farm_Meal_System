using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/task-samples")]
    [ApiController]
    public class TaskSamplesController : ControllerBase
    {
        private readonly ITaskSampleService _taskSampleService;
        public TaskSamplesController(ITaskSampleService taskSampleService)
        {
            _taskSampleService = taskSampleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTaskSamples([FromQuery] TaskSampleFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _taskSampleService.GetTaskSamples(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskSample([FromRoute] Guid id)
        {
            try
            {
                return await _taskSampleService.GetTaskSample(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskSample([FromForm] TaskSampleCreateModel model)
        {
            try
            {
                return await _taskSampleService.CreateTaskSample(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskSample([FromRoute] Guid id, [FromForm] TaskSampleUpdateModel model)
        {
            try
            {
                return await _taskSampleService.UpdateTaskSample(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
