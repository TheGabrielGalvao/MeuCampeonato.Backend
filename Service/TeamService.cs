using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Interface.Repository;
using Domain.Interface.Service;

namespace Service
{
    public class TeamService : BaseService<TeamEntity, TeamRequest, TeamResponse>, ITeamService
    {
        public TeamService(ITeamRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }
    }
}
