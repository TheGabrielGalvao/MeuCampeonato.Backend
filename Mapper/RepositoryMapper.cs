using Domain.DTO.Settings;
using Domain.Interface.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repository;

namespace Mapper
{
    public static class RepositoryMapper
    {
        public static void Map(IServiceCollection services, IConfiguration configuration, IOptions<JwtSettingsDTO> jwtSettings)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository>(provider =>
            {
                return new AuthRepository(configuration, jwtSettings);
            });
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IChampionshipRepository, ChampionshipRepository>();
            services.AddScoped<IChampionshipHistoryRepository, ChampionshipHistoryRepository>();

        }
    }
}
