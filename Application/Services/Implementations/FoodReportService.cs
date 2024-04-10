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
    public class FoodReportService : BaseService, IFoodReportService
    {
        private readonly IFoodReportRepository _foodReportRepository;
        private readonly IFoodRepository _foodRepository;
        public FoodReportService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _foodReportRepository = unitOfWork.FoodReport;
            _foodRepository = unitOfWork.Food;
        }

        public async Task<IActionResult> GetFoodReports(FoodReportFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _foodReportRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Food.Name.Contains(filter.Name));
                }
                if (filter.StaffId != null)
                {
                    query = query.Where(cg => cg.StaffId.Equals(filter.StaffId));
                }
                if (filter.FoodId != null)
                {
                    query = query.Where(cg => cg.FoodId.Equals(filter.FoodId));
                }
                if (filter.RemainQuantity != null)
                {
                    query = query.Where(cg => cg.RemainQuantity.Equals(filter.RemainQuantity));
                }
                var totalRows = query.Count();
                var foodReports = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<FoodReportViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return foodReports.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFoodReport(Guid id)
        {
            try
            {
                var foodReport = await _foodReportRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FoodReportViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return foodReport != null ? foodReport.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedFoodReport(Guid id)
        {
            try
            {
                var foodReport = await _foodReportRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FoodReportViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return foodReport != null ? foodReport.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateFoodReport(FoodReportCreateModel model)
        {
            try
            {
                var foodReport = _mapper.Map<FoodReport>(model);

                var food = await _foodRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(model.FoodId));
                food.Quantity = (double)model.RemainQuantity;

                _foodRepository.Update(food);
                _foodReportRepository.Add(foodReport);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedFoodReport(foodReport.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateFoodReport(Guid id, FoodReportUpdateModel model)
        {
            try
            {
                var foodReport = await _foodReportRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (foodReport == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }

                if (model.RemainQuantity != null)
                {
                    var food = await _foodRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(model.FoodId));
                    food.Quantity = (double)model.RemainQuantity;
                    _foodRepository.Update(food);
                }

                _mapper.Map(model, foodReport);
                _foodReportRepository.Update(foodReport);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetFoodReport(foodReport.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}