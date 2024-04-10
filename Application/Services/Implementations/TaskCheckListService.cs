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
    public class TaskCheckListService : BaseService, ITaskCheckListService
    {
        private readonly ITaskCheckListRepository _taskCheckListRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public TaskCheckListService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _taskRepository = unitOfWork.Task;
            _taskCheckListRepository = unitOfWork.TaskCheckList;
            _cloudStorageService = cloudStorageService;
        }
        public async Task<IActionResult> GetTaskCheckLists(TaskCheckListFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskCheckListRepository.GetAll();
                if (filter.Title != null)
                {
                    query = query.Where(cg => cg.Title.Contains(filter.Title));
                }
                if (filter.AsigneeId != null)
                {
                    query = query.Where(cg => cg.AsigneeId.Equals(filter.AsigneeId));
                }

                var totalRows = query.Count();

                var taskCheckList = await query
                    .AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskCheckListViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return taskCheckList.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetTaskCheckList(Guid id)
        {
            try
            {
                var taskCheckList = await _taskCheckListRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskCheckListViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;

                return taskCheckList != null ? taskCheckList.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedTaskCheckList(Guid id)
        {
            try
            {
                var taskCheckList = await _taskCheckListRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskCheckListViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskCheckList != null ? taskCheckList.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateTaskCheckList(TaskCheckListCreateModel model)
        {
            try
            {
                var taskCheckList = _mapper.Map<TaskCheckList>(model);
                _taskCheckListRepository.Add(taskCheckList);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTaskCheckList(taskCheckList.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateTaskCheckList(Guid id, TaskCheckListUpdateModel model)
        {
            try
            {
                var taskCheckList = await _taskCheckListRepository
                    .FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (taskCheckList == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Status != null && model.Status == true && await IsTaskProgress(id))
                {
                    var task = await _taskRepository.Where(ta => ta.TaskCheckLists.Any(cl => cl.Id.Equals(id))).FirstOrDefaultAsync();
                    if (task != null)
                    {
                        task.Status = TaskStatuses.IN_PROGRESS;
                        _taskRepository.Update(task);
                    }
                }
                _mapper.Map(model, taskCheckList);
                _taskCheckListRepository.Update(taskCheckList);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetTaskCheckList(taskCheckList.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> IsTaskProgress(Guid checklistId)
        {
            try
            {
                var task = await _taskRepository.Where(ta => ta.TaskCheckLists.Any(cl => cl.Id.Equals(checklistId))).FirstOrDefaultAsync();
                if (task != null && task.TaskCheckLists.All(cl => !cl.Status))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
