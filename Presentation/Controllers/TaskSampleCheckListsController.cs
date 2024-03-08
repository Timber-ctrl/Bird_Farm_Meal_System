using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/task-sample-check-lists")]
    [ApiController]
    public class TaskSampleCheckListsController : ControllerBase
    {
        private readonly ITaskSampleCheckListService _taskSampleCheckListService;
        public TaskSampleCheckListsController(ITaskSampleCheckListService taskSampleCheckListService)
        {
            _taskSampleCheckListService = taskSampleCheckListService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTaskSampleCheckLists([FromQuery] TaskSampleCheckListFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _taskSampleCheckListService.GetTaskSampleCheckLists(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskSampleCheckList([FromRoute] Guid id)
        {
            try
            {
                return await _taskSampleCheckListService.GetTaskSampleCheckList(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskSampleCheckList([FromForm] TaskSampleCheckListCreateModel model)
        {
            try
            {
                return await _taskSampleCheckListService.CreateTaskSampleCheckList(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskSampleCheckList([FromRoute] Guid id, [FromForm] TaskSampleCheckListUpdateModel model)
        {
            try
            {
                return await _taskSampleCheckListService.UpdateTaskSampleCheckList(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
