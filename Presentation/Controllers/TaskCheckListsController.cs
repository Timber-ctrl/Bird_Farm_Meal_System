using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/task-check-lists")]
    [ApiController]
    public class TaskCheckListsController : ControllerBase
    {
        private readonly ITaskCheckListService _taskCheckListService;
        public TaskCheckListsController(ITaskCheckListService taskCheckListService)
        {
            _taskCheckListService = taskCheckListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskCheckLists([FromQuery] TaskCheckListFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _taskCheckListService.GetTaskCheckLists(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskCheckList([FromRoute] Guid id)
        {
            try
            {
                return await _taskCheckListService.GetTaskCheckList(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskCheckList([FromForm] TaskCheckListCreateModel model)
        {
            try
            {
                return await _taskCheckListService.CreateTaskCheckList(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskCheckList([FromRoute] Guid id, [FromForm] TaskCheckListUpdateModel model)
        {
            try
            {
                return await _taskCheckListService.UpdateTaskCheckList(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
