using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/cages")]
    [ApiController]
    public class CagesController : ControllerBase
    {
        private readonly ICageService _cageService;
        public CagesController(ICageService cageService)
        {
            _cageService = cageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCages([FromQuery] CageFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _cageService.GetCages(filter, pagination);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCage([FromRoute] Guid id)
        {
            try
            {
                return await _cageService.GetCage(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCage([FromForm] CageCreateModel model)
        {
            try
            {
                return await _cageService.CreateCage(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCage([FromRoute] Guid id, [FromForm] CageUpdateModel model)
        {
            try
            {
                return await _cageService.UpdateCage(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
