using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IFoodReportService
    {
        Task<IActionResult> GetFoodReports(FoodReportFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetFoodReport(Guid id);
        Task<IActionResult> CreateFoodReport(FoodReportCreateModel model);
        Task<IActionResult> UpdateFoodReport(Guid id, FoodReportUpdateModel model);
    }
}