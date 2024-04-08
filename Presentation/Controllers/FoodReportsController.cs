using Application.Services.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/food-report")]
    [ApiController]
    public class FoodReportsController : ControllerBase
    {
        private readonly IFoodReportService _foodReportService;
        public FoodReportsController(IFoodReportService foodReportService)
        {
            _foodReportService = foodReportService;
        }
        [HttpGet]
        public async Task<IActionResult> GetFoodReports([FromQuery] FoodReportFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _foodReportService.GetFoodReports(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFoodReport([FromRoute] Guid id)
        {
            try
            {
                return await _foodReportService.GetFoodReport(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateFoodReport([FromBody] FoodReportCreateModel model)
        {
            try
            {
                return await _foodReportService.CreateFoodReport(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateFoodReport([FromRoute] Guid id, [FromBody] FoodReportUpdateModel model)
        {
            try
            {
                return await _foodReportService.UpdateFoodReport(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}