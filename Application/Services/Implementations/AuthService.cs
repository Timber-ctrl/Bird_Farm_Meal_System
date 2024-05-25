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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly AppSettings _appSettings;
        private readonly IManagerRepository _managerRepository;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings) : base(unitOfWork, mapper)
        {
            _appSettings = appSettings.Value;
            _adminRepository = unitOfWork.Admin;
            _staffRepository = unitOfWork.Staff;
            _managerRepository = unitOfWork.Manager;
        }

        public async Task<IActionResult> StaffAuthenticate(CertificateModel certificate)
        {
            try
            {
                // Find Staff with email and password
                if (_staffRepository.Any(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password)))
                {
                    var staff = await _staffRepository.Where(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    staff!.Role = UserRoles.STAFF;
                    var accessToken = GenerateJwtToken(staff);
                    return new TokenModel { AccessToken = accessToken }.Ok();
                }

                // Return 400 if not found
                return AppErrors.INVALID_CERTIFICATE.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> ManagerAuthenticate(CertificateModel certificate)
        {
            try
            {
                // Find manager with email and password
                if (_managerRepository.Any(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password)))
                {
                    var user = await _managerRepository.FirstOrDefaultAsync(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password));
                    var manager = _mapper.Map<AuthModel>(user);
                    manager!.Role = UserRoles.MANAGER;
                    var accessToken = GenerateJwtToken(manager);

                    var tmp =  GetManagerData(user);
                    var response = new AuthResponseModel()
                    {
                        Access_token = accessToken,
                        User = tmp,
                    };

                    return response.Ok();
                }

                // Return 400 if not found
                return AppErrors.INVALID_CERTIFICATE.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> AdminAuthenticate(CertificateModel certificate)
        {
            try
            {
                // Find manager with email and password
                if (_adminRepository.Any(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password)))
                {
                    var user = await _adminRepository.FirstOrDefaultAsync(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password));
                    var admin = _mapper.Map<AuthModel>(user);
                    admin!.Role = UserRoles.ADMIN;
                    var accessToken = GenerateJwtToken(admin);

                    var tmp = GetAdminData(user);
                    var response = new AuthResponseModel()
                    {
                        Access_token = accessToken,
                        User = tmp,
                    };

                    return response.Ok();
                }

                // Return 400 if not found
                return AppErrors.INVALID_CERTIFICATE.BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthModel> GetUser(Guid id)
        {
            try
            {
                // Find Staff in Staff table
                if (_staffRepository.Any(st => st.Id.Equals(id)))
                {
                    var Staff = await _staffRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    Staff!.Role = UserRoles.STAFF;
                    return Staff;
                }

                // Find manager in manager table
                if (_managerRepository.Any(st => st.Id.Equals(id)))
                {
                    var manager = await _managerRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    manager!.Role = UserRoles.MANAGER;
                    return manager;
                }

                // Find admin in manager table
                if (_adminRepository.Any(st => st.Id.Equals(id)))
                {
                    var admin = await _adminRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    admin!.Role = UserRoles.ADMIN;
                    return admin;
                }

                // Return null if not found any user
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateJwtToken(AuthModel auth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", auth.Id.ToString()),
                    new Claim("role", auth.Role.ToString()),
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserDataModel GetManagerData(Manager user)
        {
            return new UserDataModel()
            {
                Uuid = user.Id,
                Role = UserRoles.MANAGER,
                Data = new InfoManager()
                {
                    DisplayName = user.Name,
                    PhotoURL = user.AvatarUrl,
                    Email = user.Email,
                    Phone = user.Phone,
                }
            };
        }
        public UserDataModel GetAdminData(Admin user)
        {
            return new UserDataModel()
            {
                Uuid = user.Id,
                Role = UserRoles.ADMIN,
                Data = new InfoManager()
                {
                    DisplayName = user.Name,
                    PhotoURL = user.AvatarUrl,
                    Email = user.Email,
                }
            };
        }
    }
}
