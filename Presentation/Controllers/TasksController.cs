using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTask([FromQuery] TaskFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _taskService.GetTasks(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid id)
        {
            try
            {
                return await _taskService.GetTask(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateModel model)
        {
            try
            {
                return await _taskService.CreateTask(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("assign-staffs")]
        public async Task<IActionResult> AssignStaff([FromBody] AssignStaffCreateModel model)
        {
            try
            {
                return await _taskService.AssignStaff(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("assign-staffs/{id}")]
        public async Task<IActionResult> DeleteAssignStaff([FromRoute] Guid id, [FromQuery] Guid staffId)
        {
            try
            {
                return await _taskService.DeleteAssignStaff(id, staffId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] TaskUpdateModel model)
        {
            try
            {
                return await _taskService.UpdateTask(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
