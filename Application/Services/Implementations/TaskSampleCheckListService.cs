using Application.Services.Interfaces;
using AutoMapper;
using Data.Repositories.Interfaces;
using Data;
using Data.Repositories.Implementations;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Domain.Models.Creates;
using Domain.Entities;
using Domain.Models.Updates;

namespace Application.Services.Implementations
{
    public class TaskSampleCheckListService : BaseService , ITaskSampleCheckListService
    {
        private readonly ITaskSampleCheckListRepository _taskSampleCheckListRepository;
        public TaskSampleCheckListService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _taskSampleCheckListRepository = unitOfWork.TaskSampleCheckList;
           
        }
        public async Task<IActionResult> GetTaskSampleCheckLists(TaskSampleCheckListFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var taskSampleCheckLists = _taskSampleCheckListRepository.GetAll();
                if (filter.Title != null)
                {
                    taskSampleCheckLists = taskSampleCheckLists.Where(cg => cg.Title.Contains(filter.Title));
                }
                var totalRow = taskSampleCheckLists.Count();
                var task = await taskSampleCheckLists.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskSampleCheckListViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return task.ToPaged(pagination, totalRow).Ok();
            }
            catch (Exception) { throw; }

        }
        public async Task<IActionResult> GetTaskSampleCheckList(Guid id)
        {
            try
            {
                var taskSampleCheckList = await _taskSampleCheckListRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskSampleCheckListViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskSampleCheckList != null ? taskSampleCheckList.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetCreatedTaskSampleCheckList(Guid id)
        {
            try
            {
                var taskSampleCheckList = await _taskSampleCheckListRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TaskSampleCheckListViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskSampleCheckList != null ? taskSampleCheckList.Created() : AppErrors.NOT_FOUND.NotFound();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateTaskSampleCheckList(TaskSampleCheckListCreateModel model)
        {

            try
            {
                var taskSampleCheckList = _mapper.Map<TaskSampleCheckList>(model);
                _taskSampleCheckListRepository.Add(taskSampleCheckList);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTaskSampleCheckList(taskSampleCheckList.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IActionResult> UpdateTaskSampleCheckList(Guid id, TaskSampleCheckListUpdateModel model)
        {
            try
            {
                var taskSampleCheckList = await _taskSampleCheckListRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (taskSampleCheckList == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }

                _mapper.Map(model, taskSampleCheckList);
                _taskSampleCheckListRepository.Update(taskSampleCheckList);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetTaskSampleCheckList(taskSampleCheckList.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
