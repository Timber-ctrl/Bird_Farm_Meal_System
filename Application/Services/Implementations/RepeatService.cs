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
    public class RepeatService : BaseService , IRepeatService
    {
        private readonly IRepeatRepository _repeatRepository;
        public RepeatService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _repeatRepository = unitOfWork.Repeat;
        }

        public async Task<IActionResult> GetRepeats(RepeatFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _repeatRepository.GetAll();
                if (filter.Type != null)
                {
                    query = query.Where(cg => cg.Type.Contains(filter.Type));
                }
                if (filter.TaskId != null)
                {
                    query = query.Where(cg => cg.TaskId.Equals(filter.TaskId));
                }
                if (filter.TaskSampleId != null)
                {
                    query = query.Where(cg => cg.TaskSampleId.Equals(filter.TaskSampleId));
                }
                var totalRows = query.Count();
                var repeats = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<RepeatViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return repeats.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetRepeat(Guid id)
        {
            try
            {
                var repeat = await _repeatRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<RepeatViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return repeat != null ? repeat.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedRepeat(Guid id)
        {
            try
            {
                var repeat = await _repeatRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<RepeatViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return repeat != null ? repeat.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateRepeat(RepeatCreateModel model)
        {
            try
            {
                var repeat = _mapper.Map<Repeat>(model);
                _repeatRepository.Add(repeat);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedRepeat(repeat.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateRepeat(Guid id, RepeatUpdateModel model)
        {
            try
            {
                var repeat = await _repeatRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (repeat == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, repeat);
                _repeatRepository.Update(repeat);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetRepeat(repeat.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
