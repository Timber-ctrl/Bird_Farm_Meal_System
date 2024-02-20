using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/species")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;
        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecies([FromQuery] SpeciesFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _speciesService.GetSpecies(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSpecies([FromRoute] Guid id)
        {
            try
            {
                return await _speciesService.GetSpecies(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecies([FromForm] SpeciesCreateModel model)
        {
            try
            {
                return await _speciesService.CreateSpecies(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSpecies([FromRoute] Guid id, [FromForm] SpeciesUpdateModel model)
        {
            try
            {
                return await _speciesService.UpdateSpecies(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
