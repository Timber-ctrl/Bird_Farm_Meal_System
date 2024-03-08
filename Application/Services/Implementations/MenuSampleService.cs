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
    public class MenuSampleService : BaseService , IMenuSampleService
    {
        private readonly IMenuSampleRepository _menuSampleRepository;
        public MenuSampleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _menuSampleRepository = unitOfWork.MenuSample;
        }
        public async Task<IActionResult> GetMenuSamples(MenuSampleFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _menuSampleRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                if (filter.SpeciesId != null)
                {
                    query = query.Where(cg => cg.SpeciesId.Equals(filter.SpeciesId));
                }
                if (filter.CareModeId != null)
                {
                    query = query.Where(cg => cg.CareModeId.Equals(filter.CareModeId));
                }
                var totalRows = query.Count();
                var menuSamples = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<MenuSampleViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return menuSamples.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetMenuSample(Guid id)
        {
            try
            {
                var menuSample = await _menuSampleRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menuSample != null ? menuSample.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedMenuSample(Guid id)
        {
            try
            {
                var menuSample = await _menuSampleRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuSampleViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menuSample != null ? menuSample.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMenuSample(MenuSampleCreateModel model)
        {
            try
            {
                var menuSample = _mapper.Map<MenuSammple>(model);
                _menuSampleRepository.Add(menuSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedMenuSample(menuSample.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateMenuSample(Guid id, MenuSampleUpdateModel model)
        {
            try
            {
                var menuSample = await _menuSampleRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (menuSample == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, menuSample);
                _menuSampleRepository.Update(menuSample);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetMenuSample(menuSample.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
