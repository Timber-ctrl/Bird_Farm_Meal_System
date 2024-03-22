﻿using Application.Services.Interfaces;
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

namespace Application.Services.Implementations
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _taskRepository = unitOfWork.Task;
            _cloudStorageService = cloudStorageService;
        }
        public async Task<IActionResult> GetTasks(TaskFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _taskRepository.GetAll();
                if (filter.Title != null)
                {
                    query= query.Where(cg => cg.Title.Contains(filter.Title));
                }
                if (filter.CageId != null)
                {
                    query = query.Where(cg => cg.CageId.Equals(filter.CageId));
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
            catch (Exception) { throw; }

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