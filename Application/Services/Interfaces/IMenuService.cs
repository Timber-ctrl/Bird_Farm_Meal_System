using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IActionResult> GetMenus(MenuFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetMenu(Guid id);
        Task<IActionResult> CreateMenu(MenuCreateModel model);
        Task<IActionResult> UpdateMenu(Guid id, MenuUpdateModel model);
    }
}
