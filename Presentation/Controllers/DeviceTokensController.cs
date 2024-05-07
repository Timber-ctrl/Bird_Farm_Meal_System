using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Creates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/device-tokens")]
    [ApiController]
    public class DeviceTokensController : ControllerBase
    {
        private readonly IDeviceTokenService _deviceTokenService;
        public DeviceTokensController(IDeviceTokenService deviceTokenService)
        {
            _deviceTokenService = deviceTokenService;
        }

        [HttpPost]
        [Route("staffs")]
        [Authorize(UserRoles.STAFF)]
        public async Task<IActionResult> CreateStaffDeviceToken([FromBody] DeviceTokenCreateModel model)
        {
            try
            {
                var user = this.GetAuthenticatedUser();
                return await _deviceTokenService.CreateStaffDeviceToken(user.Id, model);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("admins")]
        [Authorize(UserRoles.ADMIN)]
        public async Task<IActionResult> CreateAdminDeviceToken([FromBody] DeviceTokenCreateModel model)
        {
            try
            {
                var admin = this.GetAuthenticatedUser();
                return await _deviceTokenService.CreateAdminDeviceToken(admin.Id, model);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("managers")]
        [Authorize(UserRoles.MANAGER)]
        public async Task<IActionResult> CreateManagerDeviceToken([FromBody] DeviceTokenCreateModel model)
        {
            try
            {
                var manager = this.GetAuthenticatedUser();
                return await _deviceTokenService.CreateManagerDeviceToken(manager.Id, model);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }
    }
}
