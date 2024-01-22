using AutoMapper;
using Domain.DTO;
using Domain.DTO.Settings;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class DependencyMapper
    {
        public static void MapDependenceInjection(IServiceCollection services, IConfiguration configuration, IOptions<JwtSettingsDTO> jwtSettings)
        {
            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository>(provider =>
            {
                return new AuthRepository(configuration, jwtSettings);
            });
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IChampionshipRepository, ChampionshipRepository>();
            services.AddScoped<IChampionshipHistoryRepository, ChampionshipHistoryRepository>();
            #endregion

            #region Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IChampionshipService, ChampionshipService>();
            services.AddTransient<IMatchService, MatchService>();
            #endregion

        }

        #region Entity
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, UserResponse>();
                cfg.CreateMap<UserRequest, UserEntity>();

                cfg.CreateMap<TeamEntity, TeamResponse>();
                cfg.CreateMap<TeamRequest, TeamEntity>();

                cfg.CreateMap<ChampionshipEntity, ChampionshipResponse>();
                cfg.CreateMap<ChampionshipRequest, ChampionshipEntity>();

                cfg.CreateMap<MatchEntity, MatchResponse>();
                cfg.CreateMap<MatchRequest, MatchEntity>();

                cfg.CreateMap<TeamResponse, OptionItemResponse>()
                .ForMember(x => x.Label, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(o => o.Uuid));

                cfg.CreateMap<ChampionshipDetailsDTO, MatchResponse>()
                .ForMember(x => x.Uuid, opt => opt.MapFrom(o => o.MatchUuid));



            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
        #endregion
    }
}
