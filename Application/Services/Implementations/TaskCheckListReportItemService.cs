using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
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
    public class TaskCheckListReportItemService : BaseService, ITaskCheckListReportItemService
    {
        private readonly ITaskCheckListReportItemRepository _taskCheckListReportItemRepository;
        public TaskCheckListReportItemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _taskCheckListReportItemRepository = unitOfWork.TaskCheckListReportItem;
        }
        public async Task<IActionResult> GetTaskCheckListReportItems(TaskCheckListReportItemFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskCheckListReportItemRepository.GetAll();
                if (filter.Issue != null)
                {
                    query = query.Where(cg => cg.Issue.Contains(filter.Issue));
                }
                if (filter.TaskCheckListReportId != null)
                {
                    query = query.Where(cg => cg.TaskCheckListReportId.Equals(filter.TaskCheckListReportId));
                }
                var totalRow = query.Count();
                var taskCheckListReportItem = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskCheckListReportItemViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return taskCheckListReportItem.ToPaged(pagination, totalRow).Ok();
            }
            catch (Exception) { throw; }
        }
        public async Task<IActionResult> GetTaskCheckListReportItem(Guid id)
        {
            try
            {
                var taskCheckListReportItem = await _taskCheckListReportItemRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskCheckListReportItemViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskCheckListReportItem != null ? taskCheckListReportItem.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetCreatedTaskCheckListReportItem(Guid id)
        {
            try
            {
                var taskCheckListReportItem = await _taskCheckListReportItemRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TaskCheckListReportItemViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskCheckListReportItem != null ? taskCheckListReportItem.Created() : AppErrors.NOT_FOUND.NotFound();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateTaskCheckListReportItem(TaskCheckListReportItemCreateModel model)
        {

            try
            {
                var taskCheckListReportItem = _mapper.Map<TaskCheckListReportItem>(model);
                _taskCheckListReportItemRepository.Add(taskCheckListReportItem);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTaskCheckListReportItem(taskCheckListReportItem.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateTaskCheckListReportItem(Guid id, TaskCheckListReportItemUpdateModel model)
        {
            try
            {
                var taskCheckListReportItem = await _taskCheckListReportItemRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (taskCheckListReportItem == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, taskCheckListReportItem);
                _taskCheckListReportItemRepository.Update(taskCheckListReportItem);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetTaskCheckListReportItem(taskCheckListReportItem.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
