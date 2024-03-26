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
    [Route("api/menu-meal-samples")]
    [ApiController]
    public class MenuMealSamplesController : ControllerBase
    {
        private readonly IMenuMealSampleService _menuMealSampleService;
        public MenuMealSamplesController(IMenuMealSampleService menuMealSampleService)
        {
            _menuMealSampleService = menuMealSampleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuMealSamples([FromQuery] MenuMealSampleFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _menuMealSampleService.GetMenuMealSamples(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMenuMealSample([FromRoute] Guid id)
        {
            try
            {
                return await _menuMealSampleService.GetMenuMealSample(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuMealSample([FromForm] MenuMealSampleCreateModel model)
        {
            try
            {
                return await _menuMealSampleService.CreateMenuMealSample(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMenuMealSample([FromRoute] Guid id, [FromForm] MenuMealSampleUpdateModel model)
        {
            try
            {
                return await _menuMealSampleService.UpdateMenuMealSample(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMenuMealSample([FromRoute] Guid id)
        {
            try
            {
                return await _menuMealSampleService.DeleteMenuMealSample(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
