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
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Updates;

namespace Application.Services.Implementations
{
    public class TaskSampleService : BaseService, ITaskSampleService
    {
        private readonly ITaskSampleRepository _taskSampleRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public TaskSampleService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _taskSampleRepository = unitOfWork.TaskSample;
            _cloudStorageService = cloudStorageService;
        }
        public async Task<IActionResult> GetTaskSamples(TaskSampleFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskSampleRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                if (filter.CareModeId != null)
                {
                    query = query.Where(cg => cg.CareModeId.Equals(filter.CareModeId));
                }
                var totalRows = query.Count();
                var taskSamples = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<TaskSampleViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return taskSamples.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetTaskSample(Guid id)
        {
            try
            {
                var taskSample = await _taskSampleRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TaskSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskSample != null ? taskSample.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedTaskSample(Guid id)
        {
            try
            {
                var taskSample = await _taskSampleRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<TaskSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return taskSample != null ? taskSample.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateTaskSample(TaskSampleCreateModel model)
        {
            try
            {
                var taskSample = _mapper.Map<TaskSample>(model);
                if (model.ThumbnailUrl != null)
                {
                    taskSample.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.ThumbnailUrl);
                }
                _taskSampleRepository.Add(taskSample); 
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedTaskSample(taskSample.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateTaskSample(Guid id, TaskSampleUpdateModel model)
        {
            try
            {
                var taskSample = await _taskSampleRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (taskSample == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.ThumbnailUrl != null)
                {
                    taskSample.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.ThumbnailUrl);
                }
                _mapper.Map(model, taskSample);
                _taskSampleRepository.Update(taskSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetTaskSample(taskSample.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
