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
    [Route("api/admins")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        // DI 
        public AdminsController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }



        [HttpGet]
        [Route("informations")]
        [Authorize("Admin")]
        public async Task<IActionResult> GetAdminInformation()
        {
            try
            {
                var auth = this.GetAuthenticatedUser();
                return await _adminServices.GetAdminInformation(auth.Id);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }


    }
}
