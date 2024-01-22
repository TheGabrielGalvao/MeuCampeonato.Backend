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
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<UserResponse> AddAsync(UserRequest request)
        {
            var user = request;
            user.UserPass = EncryptionHelper.HashPassword(user.UserPass);
            
            var addedUser = await base.AddAsync(user);

            return addedUser;
        }

        public async Task<UserResponse> GetFullUserInfo(string username)
        {
            var userInfo = await _repository.GetFullUserInfo(username);
            var teste = _mapper.Map<UserResponse>(userInfo);
            return _mapper.Map<UserResponse>(userInfo);
        }

    }
}
