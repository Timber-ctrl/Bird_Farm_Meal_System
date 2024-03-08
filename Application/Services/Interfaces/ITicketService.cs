using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IActionResult> GetTickets(TicketFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetTicket(Guid id);
        Task<IActionResult> CreateTicket(TicketCreateModel model);
        Task<IActionResult> UpdateTicket(Guid id, TicketUpdateModel model);
    }
}
