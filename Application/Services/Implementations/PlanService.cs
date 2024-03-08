using Application.Services.Interfaces;
using AutoMapper;
using Data.Repositories.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class PlanService : BaseService , IPlanService
    {
        private readonly IPlanRepository _planRepository;
        public PlanService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _planRepository = unitOfWork.Plan;
        }
        public async Task<IActionResult> GetPlans(PlanFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _planRepository.GetAll();
                
                if (filter.MenuId != null)
                {
                    query = query.Where(cg => cg.MenuId.Equals(filter.MenuId));
                }
                if (filter.CageId != null)
                {
                    query = query.Where(cg => cg.CageId.Equals(filter.CageId));
                }
                var totalRows = query.Count();
                var plans = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<PlanViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return plans.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetPlan(Guid id)
        {
            try
            {
                var plan = await _planRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<PlanViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return plan != null ? plan.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedPlan(Guid id)
        {
            try
            {
                var plan = await _planRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<PlanViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return plan != null ? plan.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreatePlan(PlanCreateModel model)
        {
            try
            {
                var plan = _mapper.Map<Plan>(model);
                _planRepository.Add(plan);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedPlan(plan.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdatePlan(Guid id, PlanUpdateModel model)
        {
            try
            {
                var plan = await _planRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (plan == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, plan);
                _planRepository.Update(plan);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetPlan(plan.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
