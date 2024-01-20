using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface ITeamRepository : IBaseRepository<TeamEntity>
    {
        Task<List<TeamEntity>> GetByUuidList(IList<Guid> selectedTeams);
    }
}
