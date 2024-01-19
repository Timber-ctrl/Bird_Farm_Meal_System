using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IManagerService
    {
        Task<IActionResult> GetManagerInformation(Guid id);
    }
}
