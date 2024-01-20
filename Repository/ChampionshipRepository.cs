using Dapper;
using Database;
using Domain.DTO;
using Domain.Entity;
using Domain.Interface.Repository;

namespace Repository
{
    public class ChampionshipRepository : BaseRepository<ChampionshipEntity>, IChampionshipRepository
    {
        public ChampionshipRepository(AppDbContext context) : base(context)
        {

        }

    }
}
