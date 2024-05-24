using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class ManagerService : BaseService, IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IAuthService _authService;
        private readonly ICloudStorageService _cloudStorageService;

        public ManagerService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _managerRepository = unitOfWork.Manager;
            _authService = authService;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetManagers(ManagerFilterModel filter, PaginationRequestModel pagination)
        {
            try
            {
                var query = _managerRepository.GetAll();
                if (filter.Name != null)
                {
                    query = query.Where(cg => cg.Name.Contains(filter.Name));
                }
                if (filter.Status != null)
                {
                    query = query.Where(cg => cg.Status.Contains(filter.Status));
                }
                var totalRows = query.Count();
                var managers = await query.AsNoTracking()
                    .Paginate(pagination)
                    .ProjectTo<ManagerViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return managers.ToPaged(pagination, totalRows).Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Manager> GetManager(Guid id)
        {
            try
            {
                var manager = await _managerRepository.Where(ma => ma.Id.Equals(id))
                    .FirstOrDefaultAsync();

                return manager != null ? manager : null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> GetManagerInformation(Guid id)
        {
            try
            {
                var manager = await _managerRepository.Where(ma => ma.Id.Equals(id))
                    .FirstOrDefaultAsync();
                if (manager == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                var authModel = _mapper.Map<AuthModel>(manager);
                authModel.Role = UserRoles.MANAGER;
                var accessToken = _authService.GenerateJwtToken(authModel);

                var response = new AuthResponseModel()
                {
                    Access_token = accessToken,
                    User = new UserDataModel
                    {
                        Uuid = manager.Id,
                        Role = UserRoles.MANAGER,
                        Data = new InfoManager
                        {
                            DisplayName = manager.Name,
                            PhotoURL = manager.AvatarUrl,
                            Email = manager.Email,
                            Phone = manager.Phone,

                        }
                    }
                };

                return response != null ? response.Ok() : AppErrors.NOT_FOUND.NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> CreateManager(ManagerRegistrationModel model)
        {
            try
            {
                // Return 409 if email conflict
                if (IsEmailExist(model.Email))
                {
                    return AppErrors.DUPLICATE_EMAIL.Conflict();
                }

                // Return 409 if phone number conflict
                if (model.Phone != null && IsPhoneNumberExist(model.Phone))
                {
                    return AppErrors.DUPLICATE_PHONE.Conflict();
                }

                // Create new manager
                var manager = _mapper.Map<Manager>(model);

                _managerRepository.Add(manager);

                // Save db
                await _unitOfWork.SaveChangesAsync();

                // Return created manager
                var createdManager = await GetManager(manager.Id);
                var authModel = _mapper.Map<AuthModel>(createdManager);
                authModel.Role = UserRoles.MANAGER;
                var accessToken = _authService.GenerateJwtToken(authModel);
                var response = new AuthResponseModel()
                {
                    Access_token = accessToken,
                    User = new UserDataModel
                    {
                        Uuid = createdManager.Id,
                        Role = UserRoles.MANAGER,
                        Data = new InfoManager
                        {
                            DisplayName = createdManager.Name,
                            PhotoURL = manager.AvatarUrl,
                            Email = createdManager.Email,
                            Phone = createdManager.Phone,

                        }
                    }
                };
                return response.Created();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateManager(Guid id, ManagerUpdateModel model)
        {
            try
            {
                var manager = await _managerRepository.FirstOrDefaultAsync(cg => cg.Id.Equals(id));
                if (manager == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                if (model.Avatar != null)
                {
                    manager.AvatarUrl = await _cloudStorageService.Upload(Guid.NewGuid(), model.Avatar);
                }
                _mapper.Map(model, manager);
                _managerRepository.Update(manager);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0 ? await GetManagerInformation(manager.Id) : AppErrors.UPDATE_FAILED.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsEmailExist(string email)
        {
            return _managerRepository.Any(ma => ma.Email.Equals(email));
        }

        private bool IsPhoneNumberExist(string phone)
        {
            return _managerRepository.Any(ma => ma.Phone != null && ma.Phone.Equals(phone));
        }

    }
}
