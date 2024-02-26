using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Models.Filters;
using Domain.Models.Pagination;
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
    }
}
