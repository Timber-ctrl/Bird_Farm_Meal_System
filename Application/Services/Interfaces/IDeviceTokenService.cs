using Domain.Models.Creates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IDeviceTokenService
    {
        Task<IActionResult> CreateStaffDeviceToken(Guid staffId, DeviceTokenCreateModel model);
        Task<IActionResult> CreateAdminDeviceToken(Guid adminId, DeviceTokenCreateModel model);
        Task<IActionResult> CreateManagerDeviceToken(Guid managerId, DeviceTokenCreateModel model);
    }
}
