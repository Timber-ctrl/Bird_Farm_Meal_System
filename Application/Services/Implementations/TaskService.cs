using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Task = Domain.Entities.Task;
using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Constants;
using Hangfire.Server;

namespace Application.Services.Implementations
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IAssignStaffRepository _assignStaffRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly INotificationService _notificationService;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService) : base(unitOfWork, mapper)
        {
            _taskRepository = unitOfWork.Task;
            _assignStaffRepository = unitOfWork.AssignStaff;
            _managerRepository = unitOfWork.Manager;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> GetTasks(TaskFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskRepository.GetAll();
                if (filter.Title != null)
                {
                    query = query.Where(cg => cg.Title.Contains(filter.Title));
                }
                if (filter.StaffId != null)
                {
                    query = query.Where(cg => cg.AssignStaffs.Any(st => st.StaffId.Equals(filter.StaffId)));
                }
                if (filter.Status != null)
                {
                    query = query.Where(cg => cg.Status.Contains(filter.Status));
                }
                if (filter.ManagerId != null)
                {
                    query = query.Where(cg => cg.ManagerId.Equals(filter.ManagerId));
                }
                if (filter.FarmId != null)
                {
                    query = query.Where(cg => cg.Manager.Farm != null && cg.Manager.Farm.Id.Equals(filter.FarmId));
                }
                var totalRow = query.Count();
                var task = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return task.ToPaged(pagination, totalRow).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetTask(Guid id)
        {
            try
            {
                var task = await _taskRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return task != null ? task.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetStaffTask(Guid id, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskRepository.Where(cg => cg.AssignStaffs.Any(at => at.StaffId.Equals(id)));
                var totalRow = query.Count();
                var task = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return task.ToPaged(pagination, totalRow).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetCreatedTask(Guid id)
        {
            try
            {
                var task = await _taskRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return task != null ? task.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateTask(TaskCreateModel model)
        {
            try
            {
                var task = _mapper.Map<Task>(model);
                _taskRepository.Add(task);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTask(task.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> AssignStaff(AssignStaffCreateModel model)
        {
            try
            {
                var assignStaff = _mapper.Map<AssignStaff>(model);
                _assignStaffRepository.Add(assignStaff);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetTask(assignStaff.TaskId) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> DeleteAssignStaff(Guid taskId, Guid staffId)
        {
            try
            {
                var assignStaff = await _assignStaffRepository.FirstOrDefaultAsync(at => at.TaskId.Equals(taskId) && at.StaffId.Equals(staffId));
                _assignStaffRepository.Remove(assignStaff);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? new NoContentResult() : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateTask(Guid id, TaskUpdateModel model)
        {
            try
            {
                var task = await _taskRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (task == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, task);
                _taskRepository.Update(task);
                var result = await _unitOfWork.SaveChangesAsync();
                if (model.Status != null) {
                    await TaskStatusNotifyForManager(task.Id, model.Status);
                }
                return result > 0 ? await GetTask(task.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async System.Threading.Tasks.Task TaskStatusNotifyForManager(Guid taskId, string status)
        {
            try
            {
                var task = await _taskRepository.Where(ta => ta.Id.Equals(taskId)).FirstOrDefaultAsync();
                if (task == null)
                {
                    return;
                }
                var notification = new NotificationCreateModel
                {
                    Title = "Task status changed",
                    Body = $"{task.Title} has changed status to {status}",
                    Type = NotificationTypes.TASK,
                    Link = task.Id.ToString(),
                };
                var managerIds = new List<Guid>()
                {
                    task.ManagerId,
                };
                await _notificationService.SendNotificationForManagers(managerIds, notification);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async System.Threading.Tasks.Task TaskStatusNotifyForStaff(Guid taskId, string status)
        {
            try
            {
                var task = await _taskRepository.Where(ta => ta.Id.Equals(taskId)).FirstOrDefaultAsync();
                if (task == null)
                {
                    return;
                }
                var notification = new NotificationCreateModel
                {
                    Title = "Task status changed",
                    Body = $"{task.Title} has changed status to {status}",
                    Type = NotificationTypes.TASK,
                    Link = task.Id.ToString(),
                };
                var staffIds = task.AssignStaffs.Select(x => x.StaffId).ToList();
                await _notificationService.SendNotificationForManagers(staffIds, notification);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}