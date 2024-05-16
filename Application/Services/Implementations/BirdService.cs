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
    public class BirdService : BaseService, IBirdService
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public BirdService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _birdRepository = unitOfWork.Bird;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetBirds(BirdFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _birdRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                if (filter.CategoryId != null)
                {
                    query = query.Where(cg => cg.CategoryId.Equals(filter.CategoryId));
                }
                if (filter.CageId != null)
                {
                    query = query.Where(cg => cg.CageId.Equals(filter.CageId));
                }      
                if (filter.SpeciesId != null)
                {
                    query = query.Where(cg => cg.SpeciesId.Equals(filter.SpeciesId));
                }
                if (filter.FarmId != null)
                {
                    query = query.Where(cg => cg.Cage.Area.FarmId.Equals(filter.FarmId));
                }
                var totalRows = query.Count();
                var birds = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<BirdViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return birds.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetBird(Guid id)
        {
            try
            {
                var bird = await _birdRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<BirdViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return bird != null ? bird.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedBird(Guid id)
        {
            try
            {
                var bird = await _birdRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<BirdViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return bird != null ? bird.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateBird(BirdCreateModel model)
        {
            try
            {
                var bird = _mapper.Map<Bird>(model);
                if (model.Thumbnail != null)
                {
                    bird.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _birdRepository.Add(bird);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedBird(bird.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateBird(Guid id, BirdUpdateModel model)
        {
            try
            {
                var bird = await _birdRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (bird == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Thumbnail != null)
                {
                    bird.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _mapper.Map(model, bird);
                _birdRepository.Update(bird);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetBird(bird.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
