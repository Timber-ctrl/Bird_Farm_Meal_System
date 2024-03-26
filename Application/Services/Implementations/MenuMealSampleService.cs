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
    public class MenuMealSampleService : BaseService , IMenuMealSampleService
    {
        private readonly IMenuMealSampleRepository _menuMealSampleRepository;
        public MenuMealSampleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _menuMealSampleRepository = unitOfWork.MenuMealSample;
        }
        public async Task<IActionResult> GetMenuMealSamples(MenuMealSampleFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _menuMealSampleRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var menuMealSamples = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<MenuMealSampleViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return menuMealSamples.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetMenuMealSample(Guid id)
        {
            try
            {
                var menuMealSample = await _menuMealSampleRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuMealSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menuMealSample != null ? menuMealSample.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedMenuMealSample(Guid id)
        {
            try
            {
                var menuMealSample = await _menuMealSampleRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuMealSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menuMealSample != null ? menuMealSample.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMenuMealSample(MenuMealSampleCreateModel model)
        {
            try
            {
                var menuMealSample = _mapper.Map<MenuMealSample>(model);
                _menuMealSampleRepository.Add(menuMealSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedMenuMealSample(menuMealSample.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateMenuMealSample(Guid id, MenuMealSampleUpdateModel model)
        {
            try
            {
                var menuMealSample = await _menuMealSampleRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (menuMealSample == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, menuMealSample);
                _menuMealSampleRepository.Update(menuMealSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetMenuMealSample(menuMealSample.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> DeleteMenuMealSample(Guid id)
        {
            try
            {
                if (!IsMenuMealSampleExist(id))
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                var menuMealSample = await _menuMealSampleRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                _menuMealSampleRepository.Remove(menuMealSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? new NoContentResult() : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsMenuMealSampleExist(Guid id)
        {
            try
            {
                return _menuMealSampleRepository.Any(mi => mi.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
