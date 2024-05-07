using Domain.Models.Authentications;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IActionResult> GetStaffs(StaffFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetStaffInformation(Guid id);
        Task<IActionResult> CreateStaff(StaffRegistrationModel model);
        Task<IActionResult> UpdateStaff(Guid id, StaffUpdateModel model);
    }
}
