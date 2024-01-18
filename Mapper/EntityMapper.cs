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
            });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}
