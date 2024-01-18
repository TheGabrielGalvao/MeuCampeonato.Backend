using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Util.Helpers;

namespace Service
{
    public class UserService : BaseService<UserEntity, UserRequest, UserResponse>, IUserService
    {
        public UserService(IUserRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            
        }

        public override async Task<UserResponse> AddAsync(UserRequest request)
        {
            var user = request;
            user.UserPass = EncryptionHelper.HashPassword(user.UserPass);
            
            var addedUser = await base.AddAsync(user);

            return addedUser;
        }

    }
}
