using Domain.Entity;

namespace Domain.Interface.Repository
{
    public interface IMatchRepository : IBaseRepository<MatchEntity>
    {
        Task<IList<MatchEntity>> GetByChampionshipId(int id);
        
    }
}
