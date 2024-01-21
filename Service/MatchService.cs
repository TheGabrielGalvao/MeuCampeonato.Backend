using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface.Repository;
using Domain.Interface.Service;

namespace Service
{
    public class MatchService : BaseService<MatchEntity, MatchRequest, MatchResponse>, IMatchService
    {
        private readonly IChampionshipHistoryRepository _championshipHistoryRepository;
        public MatchService(IMatchRepository repository, IChampionshipHistoryRepository championshipHistoryRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _championshipHistoryRepository = championshipHistoryRepository;
        }

        public async Task GenerateResultMatch(MatchEntity match)
        {
            Random random = new Random();
            match.HomeTeamNormalTimeScore = random.Next(0, 10);
            match.AwayTeamNormalTimeScore = random.Next(0, 10);

            if (match.HomeTeamNormalTimeScore == match.AwayTeamNormalTimeScore)
            {
                match.HomeTeamPenaltyScore = random.Next(0, 5);
                match.AwayTeamPenaltyScore = random.Next(0, 5);
            }

            int homeTeamTotalScore = match.HomeTeamNormalTimeScore + (match.HomeTeamPenaltyScore ?? 0);
            int awayTeamTotalScore = match.AwayTeamNormalTimeScore + (match.AwayTeamPenaltyScore ?? 0);

            match.MatchWinnerId = homeTeamTotalScore > awayTeamTotalScore ? match.HomeTeamId : match.AwayTeamId;

        }

        public async Task<List<ChampionshipDetailsDTO>> GenerateMatches(ChampionshipEntity championship, IList<TeamEntity> selectedTeams, EChampionshipStage stage)
        {

            var shuffledTeams = selectedTeams.OrderBy(t => Guid.NewGuid()).ToList();

            var seedings = shuffledTeams.Select((team, index) => new { Team = team, Seed = index + 1 }).ToList();

            var seedingGroups = seedings.GroupBy(s => (s.Seed - 1) / 2)
                                       .Select(group => group.Select(entry => entry.Team).ToList())
                                       .ToList();

            var matches = new List<MatchEntity>();
            foreach (var seedingGroup in seedingGroups)
            {
                if (seedingGroup.Count == 2)
                {
                    var match = new MatchEntity
                    {
                        HomeTeamId = (int)seedingGroup[0].Id,
                        AwayTeamId = (int)seedingGroup[1].Id,
                        ChampionshipStage = stage,
                        ChampionshipId = (int)championship.Id,
                    };

                    GenerateResultMatch(match);

                    matches.Add(match);
                }
                else
                {
                }
            }

            foreach (var match in matches)
            {
                var addedMatch = await _repository.AddAsync(match);
            }

            var response = await _championshipHistoryRepository.GetHistoryByUuid(championship.Uuid);

            return _mapper.Map<List<ChampionshipDetailsDTO>>(response);
        }
    }
}
