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
    public class MealItemSampleService : BaseService , IMealItemSampleService
    {
        private readonly IMealItemSampleRepository _mealItemSampleRepository;
        public MealItemSampleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _mealItemSampleRepository = unitOfWork.MealItemSample;
        }
        public async Task<IActionResult> GetMealItemSamples(MealItemSampleFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _mealItemSampleRepository.GetAll();
                
                if (filter.FoodId != null)
                {
                    query = query.Where(cg => cg.FoodId.Equals(filter.FoodId));
                }
                var totalRows = query.Count();
                var mealItemSamples = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<MealItemSampleViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return mealItemSamples.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetMealItemSample(Guid id)
        {
            try
            {
                var mealItemSample = await _mealItemSampleRepository.Where(cg => cg.MenuMealSampleId.Equals(id)).AsNoTracking()
                    .ProjectTo<MealItemSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return mealItemSample != null ? mealItemSample.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedMealItemSample(Guid id)
        {
            try
            {
                var mealItemSample = await _mealItemSampleRepository.Where(cg => cg.MenuMealSampleId.Equals(id)).AsNoTracking()
                    .ProjectTo<MealItemSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return mealItemSample != null ? mealItemSample.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMealItemSample(MealItemSampleCreateModel model)
        {
            try
            {
                var mealItemSample = _mapper.Map<MealItemSample>(model);
                _mealItemSampleRepository.Add(mealItemSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedMealItemSample(mealItemSample.MenuMealSampleId) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateMealItemSample(Guid id, MealItemSampleUpdateModel model)
        {
            try
            {
                var mealItemSample = await _mealItemSampleRepository.FirstOrDefaultAsync(cg => cg.MenuMealSampleId.Equals(id));
                if (mealItemSample == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, mealItemSample);
                _mealItemSampleRepository.Update(mealItemSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetMealItemSample(mealItemSample.MenuMealSampleId) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
