using Dapper;
using Database;
using Domain.DTO;
using Domain.Interface.Repository;

namespace Repository
{
    public class ChampionshipHistoryRepository : IChampionshipHistoryRepository
    {
        protected readonly AppDbContext _context;

        public ChampionshipHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ChampionshipDetailsDTO>> GetHistoryByUserUuid(Guid userUuid)
        {
            try
            {
                using var connection = _context.CreateConnection();


                var query = $"SELECT * FROM VW_ChampionshipDetails WHERE UserUuid = @UserUuid";

                return (List<ChampionshipDetailsDTO>)await connection.QueryAsync<ChampionshipDetailsDTO>(query);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IList<ChampionshipDetailsDTO>> GetHistoryByUuid(Guid Uuid)
        {
            try
            {
                using var connection = _context.CreateConnection();


                var query = $"SELECT * FROM VW_ChampionshipDetails WHERE ChampionshipUuid = @Uuid";

                return (List<ChampionshipDetailsDTO>)await connection.QueryAsync<ChampionshipDetailsDTO>(query, new {Uuid = Uuid});
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
