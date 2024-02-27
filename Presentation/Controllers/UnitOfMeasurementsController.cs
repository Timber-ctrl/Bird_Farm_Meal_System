using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Filters;
using Domain.Models.Pagination;
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
    }
}
