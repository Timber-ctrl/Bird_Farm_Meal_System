using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
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
    public class FarmService : BaseService, IFarmService
    {
        private readonly IFarmRepository _farmRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public FarmService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _farmRepository = unitOfWork.Farm;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetFarms(FarmFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _farmRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();

                // Tra ve danh sach farm kem voi phân trang
                var farms = await query.AsNoTracking()
                    // Phân trang danh sách farm
                    .Paginate(pagination)
                    // Dùng mapper để map đối tượng khi thực hiện câu query
                    .ProjectTo<FarmViewModel>(_mapper.ConfigurationProvider)
                    // Thực hiện câu query để lấy về danh sách farm
                    .ToListAsync();

                return farms.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFarm(Guid id)
        {
            try
            {
                var farm = await _farmRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FarmViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return farm != null ? farm.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedFarm(Guid id)
        {
            try
            {
                var farm = await _farmRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FarmViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return farm != null ? farm.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateFarm(FarmCreateModel model)
        {
            try
            {
                if (IsManagerHasFarm(model.ManagerId))
                {
                    return AppErrors.MANAGER_HAS_FARM.Conflict();
                }
                var farm = _mapper.Map<Farm>(model);
                if (model.Thumbnail != null)
                {
                    farm.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _farmRepository.Add(farm);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedFarm(farm.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateFarm(Guid id, FarmUpdateModel model)
        {
            try
            {
                // Dau tien truyen vao FarmId cua Farm ma minh muon Update

                // Lay Farm tu database bang FarmId (id)
                var farm = await _farmRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));

                if (farm == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }

                if (model.Thumbnail != null)
                {
                    farm.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                // Map du lieu tu FarmUpdateModel sang Farm de update xuong database
                _mapper.Map(model, farm);

                // Tuong tu cau len Update trong SQL
                _farmRepository.Update(farm);

                // Luu thay doi xuong Database tra ve so dong da duoc thay doi
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetFarm(farm.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsManagerHasFarm(Guid id)
        {
            try
            {
                return _farmRepository.Any(f => f.ManagerId.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
