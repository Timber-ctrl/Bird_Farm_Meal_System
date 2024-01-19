using Domain.Models.Authentications;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> GetUser(Guid id);
    }
}
