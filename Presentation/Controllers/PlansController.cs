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
    [Route("api/plans")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;
        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans([FromQuery] PlanFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _planService.GetPlans(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlan([FromRoute] Guid id)
        {
            try
            {
                return await _planService.GetPlan(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> GetPlanDetail([FromRoute] Guid id)
        {
            try
            {
                return await _planService.GetPlanDetail(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] PlanCreateModel model)
        {
            try
            {
                return await _planService.CreatePlan(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePlan([FromRoute] Guid id, [FromBody] PlanUpdateModel model)
        {
            try
            {
                return await _planService.UpdatePlan(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("details/{id}")]
        public async Task<IActionResult> UpdatePlanDetail([FromRoute] Guid id, [FromBody] PlanDetailUpdateModel model)
        {
            try
            {
                return await _planService.UpdatePlanDetail(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
