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
    [Route("api/menu-samples")]
    [ApiController]
    public class MenuSamplesController : ControllerBase
    {
        private readonly IMenuSampleService _menuSampleService;
        public MenuSamplesController(IMenuSampleService menuSampleService)
        {
            _menuSampleService = menuSampleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuSamples([FromQuery] MenuSampleFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _menuSampleService.GetMenuSamples(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMenuSample([FromRoute] Guid id)
        {
            try
            {
                return await _menuSampleService.GetMenuSample(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuSample([FromBody] MenuSampleCreateModel model)
        {
            try
            {
                return await _menuSampleService.CreateMenuSample(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMenuSample([FromRoute] Guid id, [FromBody] MenuSampleUpdateModel model)
        {
            try
            {
                return await _menuSampleService.UpdateMenuSample(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
