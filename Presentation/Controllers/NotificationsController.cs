using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Creates;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("staffs")]
        [Authorize(UserRoles.STAFF)]
        public async Task<IActionResult> GetStaffNotifications([FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _notificationService.GetStaffNotifications(user.Id, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> SendNotifications([FromQuery] ICollection<string> tokens, [FromBody] NotificationCreateModel model)
        {
            try
            {
                await _notificationService.SendNotification(tokens, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("admins")]
        [Authorize(UserRoles.ADMIN)]
        public async Task<IActionResult> GetAdminNotifications([FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _notificationService.GetAdminNotifications(user.Id, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("managers")]
        [Authorize(UserRoles.MANAGER)]
        public async Task<IActionResult> GetManagerNotifications([FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _notificationService.GetManagerNotifications(user.Id, pagination);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetNotification([FromRoute] Guid id)
        {
            try
            {
            return await _notificationService.GetNotification(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateNotification([FromRoute] Guid id, [FromBody] NotificationUpdateModel model)
        {
            try
            {
                return await _notificationService.UpdateNotification(id, model);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.STAFF)]
        [Route("mark-as-read/staffs")]
        public async Task<IActionResult> StaffMakeAsRead()
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _notificationService.StaffMarkAsRead(user.Id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.ADMIN)]
        [Route("mark-as-read/admins")]
        public async Task<IActionResult> AdminMakeAsRead()
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _notificationService.AdminMarkAsRead(user.Id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(UserRoles.MANAGER)]
        [Route("mark-as-read/managers")]
        public async Task<IActionResult> ManagerMakeAsRead()
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _notificationService.ManagerMarkAsRead(user.Id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteNotification([FromRoute] Guid id)
        {
            try
            {
                return await _notificationService.DeleteNotification(id);
            }
            catch (Exception ex)
            {
                return ex.Message.InternalServerError();
            }
        }
    }
}
