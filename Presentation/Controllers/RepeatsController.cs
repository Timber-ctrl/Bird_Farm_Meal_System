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
    [Route("api/repeats")]
    [ApiController]
    public class RepeatsController : ControllerBase
    {
        private readonly IRepeatService _repeatService;
        public RepeatsController(IRepeatService repeatService)
        {
            _repeatService = repeatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepeats([FromQuery] RepeatFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _repeatService.GetRepeats(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRepeat([FromRoute] Guid id)
        {
            try
            {
                return await _repeatService.GetRepeat(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRepeat([FromForm] RepeatCreateModel model)
        {
            try
            {
                return await _repeatService.CreateRepeat(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRepeat([FromRoute] Guid id, [FromForm] RepeatUpdateModel model)
        {
            try
            {
                return await _repeatService.UpdateRepeat(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
