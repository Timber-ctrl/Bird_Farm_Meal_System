using Application.Services.Interfaces;
using Common.Extensions;
using Domain.Models.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("staffs")]
        public async Task<IActionResult> StaffAuthenticate([FromBody] CertificateModel certificate)
        {
            try
            {
                return await _authService.StaffAuthenticate(certificate);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }

        [HttpPost]
        [Route("managers")]
        public async Task<IActionResult> ManagerAuthenticate([FromBody] CertificateModel certificate)
        {
            try
            {
                return await _authService.ManagerAuthenticate(certificate);
            }
            catch (Exception e)
            {
                return e.Message.InternalServerError();
            }
        }
    }
}
