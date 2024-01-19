using Database;
using Domain.Entity;
using Domain.Interface.Repository;

namespace Repository
{
    public class TeamRepository : BaseRepository<TeamEntity>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        {

        }
    }
}
