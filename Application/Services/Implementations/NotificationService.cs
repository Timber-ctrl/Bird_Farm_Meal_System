using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Common.Helpers;
using Data;
using Data.Repositories.Interfaces;
using Domain.Models.Creates;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification = Domain.Entities.Notification;

namespace Application.Services.Implementations
{
    public class NotificationService : BaseService, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IDeviceTokenRepository _deviceTokenRepository;
        private new readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _notificationRepository = unitOfWork.Notification;
            _deviceTokenRepository = unitOfWork.DeviceToken;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetAdminNotifications(Guid adminId, PaginationRequestModel pagination)
        {
            var query = _notificationRepository.Where(notification => notification.AdminId.Equals(adminId));
            var notifications = await query.AsNoTracking()
                .OrderByDescending(notification => notification.CreateAt)
                .ProjectTo<NotificationViewModel>(_mapper.ConfigurationProvider)
                .Paginate(pagination)
                .ToListAsync();
            var totalRow = query.Count();
            return notifications.ToPaged(pagination, totalRow).Ok();
        }

        public async Task<IActionResult> GetManagerNotifications(Guid managerId, PaginationRequestModel pagination)
        {
            var query = _notificationRepository.Where(notification => notification.ManagerId.Equals(managerId));
            var notifications = await query.AsNoTracking()
                .OrderByDescending(notification => notification.CreateAt)
                .ProjectTo<NotificationViewModel>(_mapper.ConfigurationProvider)
                .Paginate(pagination)
                .ToListAsync();
            var totalRow = query.Count();
            return notifications.ToPaged(pagination, totalRow).Ok();
        }

        public async Task<IActionResult> GetStaffNotifications(Guid staffId, PaginationRequestModel pagination)
        {
            var query = _notificationRepository.Where(notification => notification.StaffId.Equals(staffId));
            var notifications = await query.AsNoTracking()
                .OrderByDescending(notification => notification.CreateAt)
                .ProjectTo<NotificationViewModel>(_mapper.ConfigurationProvider)
                .Paginate(pagination)
                .ToListAsync();
            var totalRow = query.Count();
            return notifications.ToPaged(pagination, totalRow).Ok();
        }

        public async Task<IActionResult> GetNotification(Guid id)
        {
            var notification = await _notificationRepository.Where(notification => notification.Id.Equals(id))
                .ProjectTo<NotificationViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (notification == null)
            {
                return AppErrors.NOT_FOUND.NotFound();
            }

            return notification.Ok();
        }

        public async Task<IActionResult> UpdateNotification(Guid id, NotificationUpdateModel model)
        {
            var notification = await _notificationRepository.Where(notification => notification.Id.Equals(id)).FirstOrDefaultAsync();
            if (notification == null)
            {
                return AppErrors.NOT_FOUND.NotFound();
            }
            notification.IsRead = model.IsRead;
            _notificationRepository.Update(notification);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                var response = await GetNotification(id);
                return response.Ok();
            }
            return AppErrors.UPDATE_FAILED.UnprocessableEntity();
        }

        public async Task<IActionResult> ManagerMarkAsRead(Guid managerId)
        {
            var notifications = await _notificationRepository.Where(notification => notification.ManagerId.Equals(managerId)).ToListAsync();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            _notificationRepository.UpdateRange(notifications);
            var result = await _unitOfWork.SaveChangesAsync();

            return notifications.Ok();
        }

        public async Task<IActionResult> AdminMarkAsRead(Guid adminId)
        {
            var notifications = await _notificationRepository.Where(notification => notification.AdminId.Equals(adminId)).ToListAsync();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            _notificationRepository.UpdateRange(notifications);
            var result = await _unitOfWork.SaveChangesAsync();

            return notifications.Ok();
        }

        public async Task<IActionResult> StaffMarkAsRead(Guid staffId)
        {
            var notifications = await _notificationRepository.Where(notification => notification.StaffId.Equals(staffId)).ToListAsync();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            _notificationRepository.UpdateRange(notifications);
            var result = await _unitOfWork.SaveChangesAsync();

            return notifications.Ok();
        }

        public async Task SendNotification(ICollection<string> deviceTokens, NotificationCreateModel model)
        {
            try
            {
                var messageData = new Dictionary<string, string>{
                            { "type", model.Type ?? "" },
                            { "link", model.Link ?? "" },
                            { "createAt", DateTimeHelper.VnNow.ToString() },
                            { "isRead", false.ToString() },
                        };
                var message = new MulticastMessage()
                {
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = model.Title,
                        Body = model.Body,
                    },
                    Data = messageData,
                    Tokens = deviceTokens.ToArray(),
                };
                var app = FirebaseApp.DefaultInstance;
                if (FirebaseApp.DefaultInstance == null)
                {
                    app = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile("firebase-adminsdk.json")
                    });
                }
                FirebaseMessaging messaging = FirebaseMessaging.GetMessaging(app);
                await messaging.SendMulticastAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SendNotificationForStaffs(ICollection<Guid> staffIds, NotificationCreateModel model)
        {
            var deviceTokens = await _deviceTokenRepository.Where(dvt => dvt.StaffId != null
                && staffIds.Contains(dvt.StaffId.Value))
                .Select(dvt => dvt.Token).ToListAsync();
            foreach (var staffId in staffIds)
            {
                var notification = _mapper.Map<Notification>(model);
                notification.StaffId = staffId;
                _notificationRepository.Add(notification);
            }
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                if (deviceTokens.Any())
                {
                    var messageData = new Dictionary<string, string>{
                            { "type", model.Type ?? "" },
                            { "link", model.Link ?? "" },
                            { "createAt", DateTime.Now.ToString() },
                            { "isRead", false.ToString() },
                        };
                    var message = new MulticastMessage()
                    {
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = model.Title,
                            Body = model.Body,
                        },
                        Data = messageData,
                        Tokens = deviceTokens
                    };
                    var app = FirebaseApp.DefaultInstance;
                    if (FirebaseApp.DefaultInstance == null)
                    {
                        app = FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile("firebase-adminsdk.json")
                        });
                    }
                    FirebaseMessaging messaging = FirebaseMessaging.GetMessaging(app);
                    await messaging.SendMulticastAsync(message);
                }
            }
        }

        public async Task SendNotificationForAdmins(ICollection<Guid> adminIds, NotificationCreateModel model)
        {
            var deviceTokens = await _deviceTokenRepository.Where(dvt => dvt.AdminId != null
                && adminIds.Contains(dvt.AdminId.Value))
                .Select(dvt => dvt.Token).ToListAsync();
            foreach (var adminId in adminIds)
            {
                var notification = _mapper.Map<Notification>(model);
                notification.AdminId = adminId;
                _notificationRepository.Add(notification);
            }
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                if (deviceTokens.Any())
                {
                    var messageData = new Dictionary<string, string>{
                            { "type", model.Type ?? "" },
                            { "link", model.Link ?? "" },
                            { "createAt", DateTime.Now.ToString() },
                            { "isRead", false.ToString() },
                        };
                    var message = new MulticastMessage()
                    {
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = model.Title,
                            Body = model.Body,
                        },
                        Data = messageData,
                        Tokens = deviceTokens
                    };
                    var app = FirebaseApp.DefaultInstance;
                    if (FirebaseApp.DefaultInstance == null)
                    {
                        app = FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile("firebase-adminsdk.json")
                        });
                    }
                    FirebaseMessaging messaging = FirebaseMessaging.GetMessaging(app);
                    await messaging.SendMulticastAsync(message);
                }
            }
        }

        public async Task SendNotificationForManagers(ICollection<Guid> managerIds, NotificationCreateModel model)
        {
            var deviceTokens = await _deviceTokenRepository.Where(dvt => dvt.ManagerId != null
                && managerIds.Contains(dvt.ManagerId.Value))
                .Select(dvt => dvt.Token).ToListAsync();
            foreach (var managerId in managerIds)
            {
                var notification = _mapper.Map<Notification>(model);
                notification.ManagerId = managerId;
                _notificationRepository.Add(notification);
            }
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                if (deviceTokens.Any())
                {
                    var messageData = new Dictionary<string, string>{
                            { "type", model.Type ?? "" },
                            { "link", model.Link ?? "" },
                            { "createAt", DateTime.Now.ToString() },
                            { "isRead", false.ToString() },
                        };
                    var message = new MulticastMessage()
                    {
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = model.Title,
                            Body = model.Body,
                        },
                        Data = messageData,
                        Tokens = deviceTokens
                    };
                    var app = FirebaseApp.DefaultInstance;
                    if (FirebaseApp.DefaultInstance == null)
                    {
                        app = FirebaseApp.Create(new AppOptions()
                        {
                            Credential = GoogleCredential.FromFile("firebase-adminsdk.json")
                        });
                    }
                    FirebaseMessaging messaging = FirebaseMessaging.GetMessaging(app);
                    await messaging.SendMulticastAsync(message);
                }
            }
        }

        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var notification = await _notificationRepository.Where(notification => notification.Id.Equals(id)).FirstOrDefaultAsync();
            if (notification == null)
            {
                return new NotFoundResult();
            }
            _notificationRepository.Remove(notification);
            var result = await _unitOfWork.SaveChangesAsync();
            return new NoContentResult();
        }
    }
}
