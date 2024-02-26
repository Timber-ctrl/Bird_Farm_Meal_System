using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/farms")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        private readonly IFarmService _farmService;
        public FarmsController(IFarmService farmService)
        {
            _farmService = farmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFarms([FromQuery] FarmFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _farmService.GetFarms(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFarm([FromRoute] Guid id)
        {
            try
            {
                return await _farmService.GetFarm(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromForm] FarmCreateModel model)
        {
            try
            {
                return await _farmService.CreateFarm(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFarm([FromRoute] Guid id, [FromForm] FarmUpdateModel model)
        {
            try
            {
                return await _farmService.UpdateFarm(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
