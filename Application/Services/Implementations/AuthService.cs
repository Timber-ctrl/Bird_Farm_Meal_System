using Application.Services.Interfaces;
using Application.Settings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Errors;
using Common.Extensions;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Models.Authentications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly AppSettings _appSettings;
        private readonly IManagerRepository _managerRepository;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings) : base(unitOfWork, mapper)
        {
            _appSettings = appSettings.Value;
            _staffRepository = unitOfWork.Staff;
            _managerRepository = unitOfWork.Manager;
        }

        public async Task<IActionResult> StaffAuthenticate(CertificateModel certificate)
        {
            try
            {
                // Find staff with email and password
                if (_staffRepository.Any(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password)))
                {
                    var staff = await _staffRepository.Where(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    staff!.Role = UserRoles.Staff;
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
                    var manager = await _managerRepository.Where(st => st.Email.Equals(certificate.Email) && st.Password.Equals(certificate.Password))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    manager!.Role = UserRoles.Manager;
                    var accessToken = GenerateJwtToken(manager);
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

        public async Task<AuthModel> GetUser(Guid id)
        {
            try
            {
                // Find staff in staff table
                if (_staffRepository.Any(st => st.Id.Equals(id)))
                {
                    var staff = await _staffRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    staff!.Role = UserRoles.Staff;
                    return staff;
                }

                // Find manager in manager table
                if (_managerRepository.Any(st => st.Id.Equals(id)))
                {
                    var manager = await _managerRepository
                        .Where(st => st.Id.Equals(id))
                        .ProjectTo<AuthModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    manager!.Role = UserRoles.Manager;
                    return manager;
                }

                // Return null if not found any user
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateJwtToken(AuthModel auth)
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
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
