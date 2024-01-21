using Dapper;
using Database;
using Domain.DTO;
using Domain.DTO.Settings;
using Domain.Entity;
using Domain.Interface.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository
{
    public class AuthRepository : IAuthRepository
    {
        protected readonly AppDbContext _context;
        private readonly JwtSettingsDTO _jwtSettings;

        public AuthRepository(AppDbContext context, IOptions<JwtSettingsDTO> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public Task<string> GenerateToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),

                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<UserEntity> GetUserAsync(AuthRequest request)
        {
            try
            {
                using var connection = _context.CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<UserEntity>($"SELECT * FROM auth.Users WHERE Username = @Username", new { Username = request.Username, UserPass = request.Password });
            }
            catch (Exception ex) { return null; }
        }
    }
}
