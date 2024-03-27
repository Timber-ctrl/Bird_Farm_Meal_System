using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/care-modes")]
    [ApiController]
    public class CareModesController : ControllerBase
    {
        private readonly ICareModeService _careModeService;
        public CareModesController(ICareModeService careModeService)
        {
            _careModeService = careModeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCareModes([FromQuery] CareModeFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _careModeService.GetCareModes(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCareMode([FromRoute] Guid id)
        {
            try
            {
                return await _careModeService.GetCareMode(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCareMode([FromBody] CareModeCreateModel model)
        {
            try
            {
                return await _careModeService.CreateCareMode(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCareMode([FromRoute] Guid id, [FromBody] CareModeUpdateModel model)
        {
            try
            {
                return await _careModeService.UpdateCareMode(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
