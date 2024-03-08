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
    [Route("api/menu-meals")]
    [ApiController]
    public class MenuMealsController : ControllerBase
    {
        private readonly IMenuMealService _menuMealService;
        public MenuMealsController(IMenuMealService menuMealService)
        {
            _menuMealService = menuMealService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuMeals([FromQuery] MenuMealFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _menuMealService.GetMenuMeals(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMenuMeal([FromRoute] Guid id)
        {
            try
            {
                return await _menuMealService.GetMenuMeal(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuMeal([FromForm] MenuMealCreateModel model)
        {
            try
            {
                return await _menuMealService.CreateMenuMeal(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMenuMeal([FromRoute] Guid id, [FromForm] MenuMealUpdateModel model)
        {
            try
            {
                return await _menuMealService.UpdateMenuMeal(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
