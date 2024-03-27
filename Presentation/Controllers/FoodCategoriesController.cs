using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/food-categories")]
    [ApiController]
    public class FoodCategoriesController : ControllerBase
    {
        private readonly IFoodCategoryService _foodCategoryService;
        public FoodCategoriesController(IFoodCategoryService foodCategoryService)
        {
            _foodCategoryService = foodCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFoodCategories([FromQuery] FoodCategoryFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _foodCategoryService.GetFoodCategories(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFoodCategory([FromRoute] Guid id)
        {
            try
            {
                return await _foodCategoryService.GetFoodCategory(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodCategory([FromBody] FoodCategoryCreateModel model)
        {
            try
            {
                return await _foodCategoryService.CreateFoodCategory(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFoodCategory([FromRoute] Guid id, [FromBody] FoodCategoryUpdateModel model)
        {
            try
            {
                return await _foodCategoryService.UpdateFoodCategory(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
