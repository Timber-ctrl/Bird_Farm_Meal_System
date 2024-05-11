using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class TicketService : BaseService , ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly INotificationService _notificationService;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, 
            ICloudStorageService cloudStorageService, INotificationService notificationService) : base(unitOfWork, mapper)
        {
            _ticketRepository = unitOfWork.Ticket;
            _cloudStorageService = cloudStorageService;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> GetTickets(TicketFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _ticketRepository.GetAll();
                if (filter.TicketCategory != null)
                {
                    query = query.Where(cg => cg.TicketCategory.Contains(filter.TicketCategory));
                }
                if (filter.Status != null)
                {
                    query = query.Where(cg => cg.Status.Contains(filter.Status));
                }

                var totalRows = query.Count();
                var tickets = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return tickets.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetTicket(Guid id)
        {
            try
            {
                var ticket = await _ticketRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return ticket != null ? ticket.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedTicket(Guid id)
        {
            try
            {
                var ticket = await _ticketRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TicketViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return ticket != null ? ticket.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateTicket(TicketCreateModel model)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(model);
                if (model.Image != null)
                {
                    ticket.Image = await _cloudStorageService.Upload(Guid.NewGuid(), model.Image);
                }
                _ticketRepository.Add(ticket);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTicket(ticket.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateTicket(Guid id, TicketUpdateModel model)
        {
            try
            {
                var ticket = await _ticketRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                _mapper.Map(model, ticket);
                if (ticket == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Image != null)
                {
                    ticket.Image = await _cloudStorageService.Upload(Guid.NewGuid(), model.Image);
                }
                if (model.ResultImage != null)
                {
                    ticket.ResultImage = await _cloudStorageService.Upload(Guid.NewGuid(), model.ResultImage);
                }
                if (model.AssigneeId == null)
                {
                    ticket.AssigneeId = null;
                }
                _ticketRepository.Update(ticket);
                var result = await _unitOfWork.SaveChangesAsync();
                if (model.Status != null)
                {
                    await TicketStatusNotifyForManager(ticket.Id, model.Status);
                }
                return result > 0 ? await GetTicket(ticket.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async System.Threading.Tasks.Task TicketStatusNotifyForManager(Guid ticketId, string status)
        {
            try
            {
                var ticket = await _ticketRepository.Where(ta => ta.Id.Equals(ticketId)).FirstOrDefaultAsync();
                if (ticket == null)
                {
                    return;
                }
                var notification = new NotificationCreateModel
                {
                    Title = "Ticket status changed",
                    Body = $"{ticket.Title} has changed status to {status}",
                    Type = NotificationTypes.TICKET,
                    Link = ticket.Id.ToString(),
                };
                var managerIds = new List<Guid>()
                {
                    ticket.CreatorId,
                };
                await _notificationService.SendNotificationForManagers(managerIds, notification);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
