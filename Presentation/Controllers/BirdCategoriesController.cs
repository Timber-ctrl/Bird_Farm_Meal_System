using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/bird-categories")]
    [ApiController]
    public class BirdCategoriesController : ControllerBase
    {
        private readonly IBirdCategoryService _birdCategoryService;
        public BirdCategoriesController(IBirdCategoryService birdCategoryService)
        {
            _birdCategoryService = birdCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBirdCategories([FromQuery] BirdCategoryFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _birdCategoryService.GetBirdCategories(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBirdCategory([FromRoute] Guid id)
        {
            try
            {
                return await _birdCategoryService.GetBirdCategory(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBirdCategory([FromForm] BirdCategoryCreateModel model)
        {
            try
            {
                return await _birdCategoryService.CreateBirdCategory(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBirdCategory([FromRoute] Guid id, [FromForm] BirdCategoryUpdateModel model)
        {
            try
            {
                return await _birdCategoryService.UpdateBirdCategory(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
