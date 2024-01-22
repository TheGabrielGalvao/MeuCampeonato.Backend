using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Service
{
    public interface IUserService : IBaseService<UserEntity, UserRequest, UserResponse>
    {
        Task<UserResponse> GetFullUserInfo(string username);
    }
}
