using Domain.Models.Authentications;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IStaffService
    {
        Task<IActionResult> GetStaffInformation(Guid id);
        Task<IActionResult> CreateStaff(StaffRegistrationModel model);
    }
}
