using Domain.DTO;
using Domain.Entity;
using Domain.Enum;

namespace Domain.Interface.Service
{
    public interface IMatchService : IBaseService<MatchEntity, MatchRequest, MatchResponse>
    {
        Task GenerateResultMatch(MatchEntity match);

        Task<List<ChampionshipDetailsDTO>> GenerateMatches(ChampionshipEntity championship, IList<TeamEntity> selectedTeams, EChampionshipStage stage);
    }
}
