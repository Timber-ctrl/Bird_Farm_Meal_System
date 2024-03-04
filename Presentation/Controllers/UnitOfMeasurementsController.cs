using Application.Services.Implementations;
using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/unit-of-measurements")]
    [ApiController]
    public class UnitOfMeasurementsController : ControllerBase
    {
        private readonly IUnitOfMeasurementService _unitOfMeasurementService;
        public UnitOfMeasurementsController(IUnitOfMeasurementService unitOfMeasurementService)
        {
            _unitOfMeasurementService = unitOfMeasurementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnitOfMeasurements([FromQuery] UnitOfMeasurementFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _unitOfMeasurementService.GetUnitOfMeasurements(filter, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUnitOfMeasurements([FromRoute] Guid id)
        {
            try
            {
                return await _unitOfMeasurementService.GetUnitOfMeasurements(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUnitOfMeasurements([FromForm] UnitOfMeasurementCreateModel model)
        {
            try
            {
                return await _unitOfMeasurementService.CreateUnitOfMeasurements(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUnitOfMeasurements([FromRoute] Guid id, [FromForm] UnitOfMeasurementUpdateModel model)
        {
            try
            {
                return await _unitOfMeasurementService.UpdateUnitOfMeasurements(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
