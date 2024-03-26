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
    public class MenuService : BaseService, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _menuRepository = unitOfWork.Menu;
        }
        public async Task<IActionResult> GetMenus(MenuFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _menuRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var menus = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return menus.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetMenu(Guid id)
        {
            try
            {
                var menu = await _menuRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menu != null ? menu.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<IActionResult> GetCreatedMenu(Guid id)
        {
            try
            {
                var menu = await _menuRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return menu != null ? menu.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMenu(MenuCreateModel model)
        {
            try
            {
                var menu = _mapper.Map<Menu>(model);
                _menuRepository.Add(menu);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedMenu(menu.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> UpdateMenu(Guid id, MenuUpdateModel model)
        {
            try
            {
                var menu = await _menuRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (menu == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, menu);
                _menuRepository.Update(menu);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetMenu(menu.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
