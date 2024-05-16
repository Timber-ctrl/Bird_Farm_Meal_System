using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IActionResult> GetPlans(PlanFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetPlan(Guid id);
        Task<IActionResult> GetPlanDetail(Guid id);
        Task<IActionResult> CreatePlan(PlanCreateModel model);
        Task<IActionResult> UpdatePlan(Guid id, PlanUpdateModel model);
        Task<IActionResult> UpdatePlanDetail(Guid id, PlanDetailUpdateModel model);
    }
}
