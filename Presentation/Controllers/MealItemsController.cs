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
    [Route("api/meal-items")]
    [ApiController]
    public class MealItemsController : ControllerBase
    {
        private readonly IMealItemService _mealItemService;
        public MealItemsController(IMealItemService mealItemService)
        {
            _mealItemService = mealItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMealItems([FromQuery] MealItemFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _mealItemService.GetMealItems(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMealItem([FromRoute] Guid id)
        {
            try
            {
                return await _mealItemService.GetMealItem(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMealItem([FromBody] MealItemCreateModel model)
        {
            try
            {
                return await _mealItemService.CreateMealItem(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMealItem([FromRoute] Guid id, [FromBody] MealItemUpdateModel model)
        {
            try
            {
                return await _mealItemService.UpdateMealItem(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMealItem([FromRoute] Guid id)
        {
            try
            {
                return await _mealItemService.DeleteMealItem(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
