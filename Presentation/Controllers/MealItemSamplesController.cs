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
    [Route("api/meal-item-samples")]
    [ApiController]
    public class MealItemSamplesController : ControllerBase
    {
        private readonly IMealItemSampleService _mealItemSampleService;
        public MealItemSamplesController(IMealItemSampleService mealItemSampleService)
        {
            _mealItemSampleService = mealItemSampleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMealItemSamples([FromQuery] MealItemSampleFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _mealItemSampleService.GetMealItemSamples(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMealItemSample([FromRoute] Guid id)
        {
            try
            {
                return await _mealItemSampleService.GetMealItemSample(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMealItemSample([FromForm] MealItemSampleCreateModel model)
        {
            try
            {
                return await _mealItemSampleService.CreateMealItemSample(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMealItemSample([FromRoute] Guid id, [FromForm] MealItemSampleUpdateModel model)
        {
            try
            {
                return await _mealItemSampleService.UpdateMealItemSample(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMealItemSample([FromRoute] Guid id)
        {
            try
            {
                return await _mealItemSampleService.DeleteMealItemSample(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
