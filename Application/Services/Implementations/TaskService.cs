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

namespace Application.Services.Implementations
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IAssignStaffRepository _assignStaff;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _taskRepository = unitOfWork.Task;
            _assignStaff = unitOfWork.AssignStaff;
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
                if (filter.Status != null)
                {
                    query = query.Where(cg => cg.Status.Contains(filter.Status));
                }
                if (filter.ManagerId != null)
                {
                    query = query.Where(cg => cg.ManagerId.Equals(filter.ManagerId));
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

        public async Task<IActionResult> GetStaffTask(Guid id)
        {
            try
            {
                var task = await _taskRepository.Where(cg => cg.AssignStaffs.Any(at => at.StaffId.Equals(id)))
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
                _assignStaff.Add(assignStaff);
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
                var assignStaff = await _assignStaff.FirstOrDefaultAsync(at => at.TaskId.Equals(taskId) && at.StaffId.Equals(staffId));
                _assignStaff.Remove(assignStaff);
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
                return result > 0 ? await GetTask(task.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}