using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/managers")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        [Route("informations")]
        [Authorize(UserRoles.Manager)]
        public async Task<IActionResult> GetManagerInformation()
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _managerService.GetManagerInformation(auth.Id);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }
    }
}
