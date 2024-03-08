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
using Domain.Entities;
using Common.Errors;
using Domain.Models.Creates;
using Domain.Models.Updates;

namespace Application.Services.Implementations
{
    public class TaskCheckListReportService : BaseService, ITaskCheckListReportService
    {
        private readonly ITaskCheckListReportRepository _taskCheckListReportRepository;
        public TaskCheckListReportService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _taskCheckListReportRepository = unitOfWork.TaskCheckListReport;
        }
        public async Task<IActionResult> GetTaskCheckListReports(TaskCheckListReportFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskCheckListReportRepository.GetAll();
                if (filter.TaskCheckListId != null)
                {
                    query = query.Where(cg => cg.TaskCheckListId.Equals(filter.TaskCheckListId));
                }
                var totalRows = query.Count();
                var taskCheckListReports = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskCheckListReportViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return taskCheckListReports.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetTaskCheckListReport(Guid id)
        {
            try
            {
                var taskCheckListReport = await _taskCheckListReportRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskCheckListReportViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;

                return taskCheckListReport != null ? taskCheckListReport.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedTaskCheckListReport(Guid id)
        {
            try
            {
                var taskCheckListReport = await _taskCheckListReportRepository.Where(cg => cg.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<TaskCheckListReportViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskCheckListReport != null ? taskCheckListReport.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateTaskCheckListReport(TaskCheckListReportCreateModel model)
        {
            try
            {
                var taskCheckListReport = _mapper.Map<TaskCheckListReport>(model);
                _taskCheckListReportRepository.Add(taskCheckListReport);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTaskCheckListReport(taskCheckListReport.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateTaskCheckListReport(Guid id, TaskCheckListReportUpdateModel model)
        {
            try
            {
                var taskCheckListReport = await _taskCheckListReportRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (taskCheckListReport == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                
                _mapper.Map(model, taskCheckListReport);
                _taskCheckListReportRepository.Update(taskCheckListReport);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetTaskCheckListReport(taskCheckListReport.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
