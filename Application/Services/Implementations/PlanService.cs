using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Common.Helpers;
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
    public class PlanService : BaseService, IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IPlanDetailRepository _planDetailRepository;
        public PlanService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _planRepository = unitOfWork.Plan;
            _planDetailRepository = unitOfWork.PlanDetail;
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

        public async Task<IActionResult> GetPlanDetail(Guid id)
        {
            try
            {
                var planDetail = await _planDetailRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<PlanDetailViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return planDetail != null ? planDetail.Ok() : AppErrors.NOT_FOUND.NotFound();
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
                plan.PlanDetails = PlanHelper.GeneratePlanDetail(plan);
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
                if (model.From != null && model.To != null)
                {
                    if (plan.From != model.From || plan.To != model.To)
                    {
                        var planDetails = await _planDetailRepository.Where(x => x.PlanId.Equals(id)).ToListAsync();
                        _planDetailRepository.RemoveRange(planDetails);
                        await _unitOfWork.SaveChangesAsync();
                        plan.From = (DateTime)model.From;
                        plan.To = (DateTime)model.To;
                        plan.PlanDetails = PlanHelper.GeneratePlanDetail(plan);
                    }
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

        public async Task<IActionResult> UpdatePlanDetail(Guid id, PlanDetailUpdateModel model)
        {
            try
            {
                var planDetail = await _planDetailRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (planDetail == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, planDetail);
                _planDetailRepository.Update(planDetail);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetPlanDetail(planDetail.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
