using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> Get(AuthRequest request);
    }
}
