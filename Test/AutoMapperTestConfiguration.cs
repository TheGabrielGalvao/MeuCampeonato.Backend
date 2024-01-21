using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Mapper;

namespace Test
{
    public class AutoMapperTestConfiguration
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
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

            return mapperConfiguration.CreateMapper();
        }
    }
}
