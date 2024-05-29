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
    public class AdminServices : BaseService, IAdminServices
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthService _authService;
        private readonly ICloudStorageService _cloudStorageService;

        public AdminServices(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, ICloudStorageService cloudStorageService) : base(unitOfWork, mapper)
        {
            _adminRepository = unitOfWork.Admin;
            _authService = authService;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> GetAdminInformation(Guid id)
        {
            try
            {
                var admin = await _adminRepository.Where(ad => ad.Id.Equals(id))
                    .FirstOrDefaultAsync();
                if (admin == null)
                {
                    return AppErrors.NOT_FOUND.NotFound();
                }
                var authModel = _mapper.Map<AuthModel>(admin);
                authModel.Role = UserRoles.ADMIN;
                var accessToken = _authService.GenerateJwtToken(authModel);

                var response = new AuthResponseModel()
                {
                    Access_token = accessToken,
                    User = new UserDataModel
                    {
                        Uuid = admin.Id,
                        Role = UserRoles.ADMIN,
                        Data = new InfoManager
                        {
                            DisplayName = admin.Name,
                            PhotoURL = admin.AvatarUrl,
                            Email = admin.Email,

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
    }
}
