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
    public class SpeciesService : BaseService, ISpeciesService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public SpeciesService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _speciesRepository = unitOfWork.Species;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetSpecies(SpeciesFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _speciesRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                var totalRows = query.Count();
                var species = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<SpeciesViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return species.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetSpecies(Guid id)
        {
            try
            {
                var species = await _speciesRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<SpeciesViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return species != null ? species.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedSpecies(Guid id)
        {
            try
            {
                var species = await _speciesRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<SpeciesViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return species != null ? species.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateSpecies(SpeciesCreateModel model)
        {
            try
            {
                var species = _mapper.Map<Species>(model);
                if (model.Thumbnail != null)
                {
                    species.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _speciesRepository.Add(species);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedSpecies(species.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateSpecies(Guid id, SpeciesUpdateModel model)
        {
            try
            {
                var species = await _speciesRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (species == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Thumbnail != null)
                {
                    species.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _mapper.Map(model, species);
                _speciesRepository.Update(species);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetSpecies(species.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
