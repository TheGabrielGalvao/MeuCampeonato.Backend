using Database;
using Domain.DTO;
using Domain.Entity;
using Domain.Interface.Repository;

namespace Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }

        public Task<UserEntity> Get(AuthRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
