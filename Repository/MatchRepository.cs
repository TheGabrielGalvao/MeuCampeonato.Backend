using Azure.Core;
using Dapper;
using Database;
using Domain.Entity;
using Domain.Interface.Repository;

namespace Repository
{
    public class MatchRepository : BaseRepository<MatchEntity>, IMatchRepository
    {
        public MatchRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IList<MatchEntity>> GetByChampionshipId(int id)
        {
            try
            {
                using var connection = _context.CreateConnection();

                
                var query = $"SELECT * FROM Match WHERE ChampionshipId = {id}";

                return (List<MatchEntity>)await connection.QueryAsync<TeamEntity>(query);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
