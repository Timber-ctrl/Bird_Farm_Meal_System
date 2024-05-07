using Application.Services.Interfaces;
using AutoMapper;
using Common.Errors;
using Common.Extensions;
using Common.Helpers;
using Data;
using Data.Repositories.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class DeviceTokenService : BaseService, IDeviceTokenService
    {
        private readonly IDeviceTokenRepository _deviceTokenRepository;

        public DeviceTokenService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _deviceTokenRepository = unitOfWork.DeviceToken;
        }

        public async Task<IActionResult> CreateStaffDeviceToken(Guid staffId, DeviceTokenCreateModel model)
        {
            var deviceTokens = await _deviceTokenRepository.Where(dvt => dvt.StaffId.Equals(staffId))
            .ToListAsync();

            if (deviceTokens.Any(token => token.Token.Equals(model.DeviceToken)))
            {
                return AppErrors.DEVICE_TOKEN_EXIST.Ok();
            }

            var deviceToken = new DeviceToken
            {
                Id = Guid.NewGuid(),
                StaffId = staffId,
                CreateAt = DateTimeHelper.VnNow,
                Token = model.DeviceToken,
            };

            _deviceTokenRepository.Add(deviceToken);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return deviceToken.Created();
            }

            return AppErrors.CREATE_FAILED.UnprocessableEntity();
        }

        public async Task<IActionResult> CreateAdminDeviceToken(Guid adminId, DeviceTokenCreateModel model)
        {
            var deviceTokens = await _deviceTokenRepository.Where(dvt => dvt.AdminId.Equals(adminId))
            .ToListAsync();

            if (deviceTokens.Any(token => token.Token.Equals(model.DeviceToken)))
            {
                return AppErrors.DEVICE_TOKEN_EXIST.Ok();
            }

            var deviceToken = new DeviceToken
            {
                Id = Guid.NewGuid(),
                AdminId = adminId,
                CreateAt = DateTimeHelper.VnNow,
                Token = model.DeviceToken,
            };

            _deviceTokenRepository.Add(deviceToken);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return deviceToken.Created();
            }

            return AppErrors.CREATE_FAILED.UnprocessableEntity();
        }

        public async Task<IActionResult> CreateManagerDeviceToken(Guid managerId, DeviceTokenCreateModel model)
        {
            var deviceTokens = await _deviceTokenRepository.Where(dvt => dvt.ManagerId.Equals(managerId))
            .ToListAsync();

            if (deviceTokens.Any(token => token.Token.Equals(model.DeviceToken)))
            {
                return AppErrors.DEVICE_TOKEN_EXIST.Ok();
            }

            var deviceToken = new DeviceToken
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                CreateAt = DateTimeHelper.VnNow,
                Token = model.DeviceToken,
            };

            _deviceTokenRepository.Add(deviceToken);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                return deviceToken.Created();
            }

            return AppErrors.CREATE_FAILED.UnprocessableEntity();
        }

    }
}
