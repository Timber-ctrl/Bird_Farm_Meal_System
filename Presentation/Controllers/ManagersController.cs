using Application.Services.Implementations;
using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Constants;
using Domain.Models.Authentications;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/managers")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;
        // DI 
        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers([FromQuery] ManagerFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _managerService.GetManagers(filter, pagination);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpGet]
        [Route("informations")]
        [Authorize(UserRoles.MANAGER)]
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

        [HttpPost]
        [Route("registrations")]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerRegistrationModel model)
        {
            try
            {
                return await _managerService.CreateManager(model);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateManager([FromRoute] Guid id, [FromForm] ManagerUpdateModel model)
        {
            try
            {
                return await _managerService.UpdateManager(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
