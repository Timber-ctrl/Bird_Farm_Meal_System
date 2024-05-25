using Domain.Models.Authentications;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IActionResult> StaffAuthenticate(CertificateModel certificate);
        Task<IActionResult> ManagerAuthenticate(CertificateModel certificate);
        Task<IActionResult> AdminAuthenticate(CertificateModel certificate);
        Task<AuthModel> GetUser(Guid id);
        string GenerateJwtToken(AuthModel auth);
    }
}
