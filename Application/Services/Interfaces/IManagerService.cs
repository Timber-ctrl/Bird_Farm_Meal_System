using Domain.Models.Authentications;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IManagerService
    {
        Task<IActionResult> GetManagerInformation(Guid id);
        Task<IActionResult> CreateManager(ManagerRegistrationModel model);
        Task<IActionResult> UpdateManager(Guid id, ManagerUpdateModel model);
    }
}
