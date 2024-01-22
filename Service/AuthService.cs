using AutoMapper;
using Domain.DTO;
using Domain.DTO.Settings;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Microsoft.Extensions.Options;
using Util.Helpers;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IUserRepository userRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            
        }
        public Task<bool> CheckToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> ExecuteAuth(AuthRequest request)
        {
            var user = await _userRepository.GetUserAsync(request);

            if (user == null || !EncryptionHelper.VerifyPassword(request.Password, user.UserPass))
            {
                return new AuthResponse { Success = false, Message = "Usuário ou Senha inválido" };
            }

            var token = await _authRepository.GenerateToken(user);

            var response = new AuthResponse
            {
                AccessToken = token,
                Success = true,
            };

            return response;
        }
    
    }
}
