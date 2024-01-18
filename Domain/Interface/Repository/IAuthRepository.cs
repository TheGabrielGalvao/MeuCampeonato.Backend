using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IAuthRepository
    {
        Task<UserEntity> GetUserAsync(AuthRequest request);
        public Task<string> GenerateToken(UserEntity customer);
    }
}
