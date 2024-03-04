using Application.Services.Interfaces;
using Application.Settings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Google.Apis.Requests.BatchRequest;

namespace Application.Services.Implementations
{
    public class ManagerService : BaseService, IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IAuthService _authService;
        public ManagerService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService) : base(unitOfWork, mapper)
        {
            _managerRepository = unitOfWork.Manager;
            _authService = authService;
        }

        private async Task<Manager> GetManager(Guid id)
        {
            try
            {
                var manager = await _managerRepository.Where(ma => ma.Id.Equals(id))
                    //ToList;
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
                    .ProjectTo<ManagerViewModel>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                return manager != null ? manager.Ok() : AppErrors.NOT_FOUND.NotFound();
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
                if (IsEmailEximas(model.Email))
                {
                    return AppErrors.DUPLICATE_EMAIL.Conflict();
                }

                // Return 409 if phone number conflict
                if (model.Phone != null && IsPhoneNumberEximas(model.Phone))
                {
                    return AppErrors.DUPLICATE_PHONE.Conflict();
                }

                // Create new manager
                var manager = _mapper.Map<Manager>(model);

                _managerRepository.Add(manager);

                // Return created manager
                var createdManager = await GetManager(manager.Id);
                var authModel = _mapper.Map<AuthModel>(createdManager);
                authModel.Role = UserRoles.MANAGER;
                var accessToken =_authService.GenerateJwtToken(authModel);  
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
                // Save db
                await _unitOfWork.SaveChangesAsync();

                return response.Created();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsEmailEximas(string email)
        {
            return _managerRepository.Any(ma => ma.Email.Equals(email));
        }

        private bool IsPhoneNumberEximas(string phone)
        {
            return _managerRepository.Any(ma => ma.Phone != null && ma.Phone.Equals(phone));
        }

    }
}
