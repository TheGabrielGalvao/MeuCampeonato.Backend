using Domain.DTO;

namespace Domain.Interface.Service
{
    public interface IAuthService
    {
        Task<AuthResponse> ExecuteAuth(AuthRequest request);

        Task<bool> CheckToken(string token);
    }
}
