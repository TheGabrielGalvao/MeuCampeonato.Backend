using Domain.DTO;
using Domain.Entity;

namespace Domain.Interface.Service
{
    public interface ITeamService : IBaseService<TeamEntity, TeamRequest, TeamResponse>
    {
    }
}
