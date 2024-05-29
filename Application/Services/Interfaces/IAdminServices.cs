using Domain.Models.Authentications;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IAdminServices
    {
        Task<IActionResult> GetAdminInformation(Guid id);
    }
}
