using Domain.Models.Creates;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IActionResult> GetStaffNotifications(Guid staffId, PaginationRequestModel pagination);
        Task<IActionResult> GetAdminNotifications(Guid adminId, PaginationRequestModel pagination);
        Task<IActionResult> GetManagerNotifications(Guid managerId, PaginationRequestModel pagination);
        Task<IActionResult> GetNotification(Guid id);
        Task SendNotification(ICollection<string> deviceTokens, NotificationCreateModel model);
        Task<IActionResult> UpdateNotification(Guid id, NotificationUpdateModel model);
        Task<IActionResult> StaffMarkAsRead(Guid staffId);
        Task<IActionResult> AdminMarkAsRead(Guid adminId);
        Task<IActionResult> ManagerMarkAsRead(Guid managerId);
        Task SendNotificationForStaffs(ICollection<Guid> staffIds, NotificationCreateModel model);
        Task SendNotificationForAdmins(ICollection<Guid> adminIds, NotificationCreateModel model);
        Task SendNotificationForManagers(ICollection<Guid> managerIds, NotificationCreateModel model);
        Task<IActionResult> DeleteNotification(Guid id);
    }
}
