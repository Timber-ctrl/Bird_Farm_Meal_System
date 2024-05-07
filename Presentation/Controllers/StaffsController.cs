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
    [Route("api/staffs")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffs([FromQuery] StaffFilterModel filter, [FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                return await _staffService.GetStaffs(filter, pagination);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("informations")]
        [Authorize(UserRoles.STAFF)]
        public async Task<IActionResult> GetStaffInformation()
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _staffService.GetStaffInformation(auth.Id);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("registrations")]
        public async Task<IActionResult> RegisterStaff([FromBody] StaffRegistrationModel model)
        {
            try
            {
                return await _staffService.CreateStaff(model);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStaff([FromRoute] Guid id, [FromForm] StaffUpdateModel model)
        {
            try
            {
                return await _staffService.UpdateStaff(id, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
