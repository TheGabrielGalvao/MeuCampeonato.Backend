using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IAuthRepository
    {
        public Task<string> GenerateToken(UserEntity customer);
    }
}
