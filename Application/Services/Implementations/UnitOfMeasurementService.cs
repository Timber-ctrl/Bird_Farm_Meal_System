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
    public class UnitOfMeasurementService : BaseService, IUnitOfMeasurementService
    {
        private readonly IUnitOfMeasurementRepository _unitOfMeasurementRepository;

        public UnitOfMeasurementService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfMeasurementRepository = unitOfWork.UnitOfMeasurement;
        }

        public async Task<IActionResult> GetUnitOfMeasurements(UnitOfMeasurementFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _unitOfMeasurementRepository.GetAll();

                if (filter.Name != null)
                {
                    query = query.Where(uom => uom.Name.Contains(filter.Name));
                }

                var totalRows = query.Count();
                var unitOfMeasurements = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<UnitOfMeasurementViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return unitOfMeasurements.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> GetUnitOfMeasurements(Guid id)
        {
            try
            {
                var unitOfMeasurement = await _unitOfMeasurementRepository
                    .Where(area => area.Id.Equals(id)).FirstOrDefaultAsync();

                return unitOfMeasurement != null ?
                    unitOfMeasurement.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch(Exception )
            {
                throw ;
            }
        }

        public async Task<IActionResult> GetCreateUnitOfMeasurements(Guid id)
        {
            try {
                var unit = await _unitOfMeasurementRepository
                    .Where(unit => unit.Id.Equals(id))
                    .AsNoTracking()
                    .ProjectTo<UnitOfMeasurementViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null;
                return unit != null ? unit.Created() : AppErrors.NOT_FOUND.NotFound();
                    
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateUnitOfMeasurements(UnitOfMeasurementCreateModel model)
        {
            try {
                var unitOfMeasurements = _mapper.Map<UnitOfMeasurement>(model);
                _unitOfMeasurementRepository.Add(unitOfMeasurements);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreateUnitOfMeasurements(unitOfMeasurements.Id) : AppErrors.CREATE_FAILED.BadRequest();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateUnitOfMeasurements(Guid id, UnitOfMeasurementUpdateModel model)
        {
            try {
                var unit = await _unitOfMeasurementRepository.FirstOrDefaultAsync(unit => unit.Id.Equals(id));
                if (unit == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                _mapper.Map(model, unit);
                _unitOfMeasurementRepository.Update(unit);
                var result = await _unitOfWork.SaveChangesAsync();

                return result >= 0 ? await GetCreateUnitOfMeasurements(unit.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
