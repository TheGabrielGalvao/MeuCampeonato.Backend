using Dapper;
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

        public async Task<List<TeamEntity>> GetByUuidList(IList<Guid> selectedTeams)
        {
            try
            {
                using var connection = _context.CreateConnection();

                var uuidsString = string.Join(",", selectedTeams.Select(id => $"'{id}'"));

                var query = $"SELECT * FROM Team WHERE Uuid IN ({uuidsString})";

                return (List<TeamEntity>)await connection.QueryAsync<TeamEntity>(query);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
