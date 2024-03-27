using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/task-check-list-report-items")]
    [ApiController]
    public class TaskCheckListReportItemsController : ControllerBase
    {
        private readonly ITaskCheckListReportItemService _taskCheckListReportItemService;
        public TaskCheckListReportItemsController(ITaskCheckListReportItemService taskCheckListReportItemService)
        {
            _taskCheckListReportItemService = taskCheckListReportItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTaskCheckListReportItem([FromQuery] TaskCheckListReportItemFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _taskCheckListReportItemService.GetTaskCheckListReportItems(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTaskCheckListReportItem([FromRoute] Guid id)
        {
            try
            {
                return await _taskCheckListReportItemService.GetTaskCheckListReportItem(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTaskCheckListReportItem([FromBody] TaskCheckListReportItemCreateModel model)
        {
            try
            {
                return await _taskCheckListReportItemService.CreateTaskCheckListReportItem(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskCheckListReportItem([FromRoute] Guid id, [FromBody] TaskCheckListReportItemUpdateModel model)
        {
            try
            {
                return await _taskCheckListReportItemService.UpdateTaskCheckListReportItem(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
