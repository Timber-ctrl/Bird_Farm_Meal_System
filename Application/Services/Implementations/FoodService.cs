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
    public class FoodService : BaseService, IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IPlanDetailRepository _planDetailRepository;
        private readonly ICloudStorageService _cloudStorageService;
        public FoodService(IUnitOfWork unitOfWork, IMapper mapper, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _foodRepository = unitOfWork.Food;
            _planDetailRepository = unitOfWork.PlanDetail;
            _planRepository = unitOfWork.Plan;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetFoods(FoodFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _foodRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                if (filter.Status != null)
                {
                    query = query.Where(cg => cg.Status.Contains(filter.Status));
                }
                if (filter.FarmId != null)
                {
                    query = query.Where(cg => cg.FarmId.Equals(filter.FarmId));
                }
                var totalRows = query.Count();
                var foods = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<FoodViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return foods.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFood(Guid id)
        {
            try
            {
                var food = await _foodRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FoodViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return food != null ? food.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IActionResult> GetCreatedFood(Guid id)
        {
            try
            {
                var food = await _foodRepository.Where(cg => cg.Id.Equals(id)).AsNoTracking()
                    .ProjectTo<FoodViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync() ?? null!;
                return food != null ? food.Created() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateFood(FoodCreateModel model)
        {
            try
            {
                var food = _mapper.Map<Food>(model);
                food.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                _foodRepository.Add(food);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetCreatedFood(food.Id) : AppErrors.CREATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateFood(Guid id, FoodUpdateModel model)
        {
            try
            {
                var food = await _foodRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (food == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Thumbnail != null)
                {
                    food.ThumbnailUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Thumbnail);
                }
                _mapper.Map(model, food);
                _foodRepository.Update(food);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetFood(food.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetFoodsByMealPlan(FoodFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var today = DateTime.Today;
                var plans = _planRepository.GetAll()
                        .Include(p => p.Menu)
                        .ThenInclude(m => m.MenuMeals)
                        .ThenInclude(mm => mm.MealItems)
                        .ThenInclude(mi => mi.Food)
                        .ToList();

                if (filter.FarmId != null)
                {
                    plans = plans.Where(plan => plan.Cage.Area.FarmId.Equals(filter.FarmId)).ToList();
                }
                // Lay tat ca plan cua ngay hom nay
                plans = plans.Where(plan => plan.From.Date <= today && plan.To.Date >= today).ToList();

                var planDetails = _planDetailRepository.GetAll().ToList();

                // Lọc các chi tiết kế hoạch có ngay hôm nay và trạng thái là true
                var todayPlanDetails = planDetails
                    .Where(pd => plans.Select(plan => plan.Id).Contains(pd.PlanId)
                                    && pd.Date.Date == today
                                    && pd.Status == true)
                    .ToList();

                var filteredPlanIds = todayPlanDetails.Select(pd => pd.PlanId).Distinct();
                var filteredPlans = plans.Where(plan => filteredPlanIds.Contains(plan.Id)).ToList();

                var foodQuantityDictionary = new Dictionary<Guid, FoodMealPlanViewModel>();

                foreach (var plan in filteredPlans)
                {
                    if (plan.Menu != null && plan.Menu.MenuMeals != null)
                    {
                        foreach (var meal in plan.Menu.MenuMeals)
                        {
                            if (meal.MealItems != null)
                            {
                                foreach (var item in meal.MealItems)
                                {
                                    var foodId = item.Food.Id; // Assumes Food.Id is of type GUID

                                    if (foodQuantityDictionary.TryGetValue(foodId, out var foodWithPlan))
                                    {
                                        foodWithPlan.PlanQuantity += item.Quantity;
                                    }
                                    else
                                    {
                                        foodQuantityDictionary[foodId] = new FoodMealPlanViewModel
                                        {
                                            Food = _mapper.Map<FoodViewModel>(item.Food),
                                            PlanQuantity = item.Quantity
                                        };
                                    }
                                }
                            }
                        }
                    }
                }

                var uniqueFoodListWithQuantity = foodQuantityDictionary.Values.ToList() ;

                return uniqueFoodListWithQuantity.Ok();
            }
            catch (Exception ex)
            {
                // handle exceptions here, e.g., log the error
                throw;
            }
        }
    }
}
