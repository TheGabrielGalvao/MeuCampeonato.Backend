using AutoMapper;
using Domain.DTO;
using Domain.Entity;

namespace Mapper
{
    public static class EntityMapper
    {
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, UserResponse>();
                cfg.CreateMap<UserRequest, UserEntity>();
                
                cfg.CreateMap<TeamEntity, TeamResponse>();
                cfg.CreateMap<TeamRequest, TeamEntity>();

                cfg.CreateMap<TeamResponse, OptionItemResponse>()
                .ForMember(x => x.Label, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(o => o.Uuid));

            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}
