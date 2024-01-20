using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface.Repository;
using Domain.Interface.Service;

namespace Service
{
    public class ChampionshipService : BaseService<ChampionshipEntity, ChampionshipRequest, ChampionshipResponse>, IChampionshipService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IChampionshipRepository _championshipRepository;
        private readonly IChampionshipHistoryRepository _championshipHistoryRepository;

        public ChampionshipService(IChampionshipRepository repository, IUserRepository userRepository, ITeamRepository teamRepository, IMatchRepository matchRepository, IChampionshipHistoryRepository championshipHistoryRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _matchRepository = matchRepository;
            _championshipHistoryRepository = championshipHistoryRepository;
        }

        

        public async Task GenerateResultMatch(MatchEntity match)
        {
            Random random = new Random();
            match.HomeTeamNormalTimeScore = random.Next(0, 8);
            match.AwayTeamNormalTimeScore = random.Next(0, 8);

            if(match.HomeTeamNormalTimeScore == match.AwayTeamNormalTimeScore)
            {
                match.HomeTeamPenaltyScore = random.Next(0, 15);
                match.AwayTeamPenaltyScore = random.Next(0, 15);
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

            foreach(var match in matches)
            {
                var addedMatch = await _matchRepository.AddAsync(match);
            }

            return (List<ChampionshipDetailsDTO>)await _championshipHistoryRepository.GetHistoryByUuid(championship.Uuid);
        }

        public async Task<List<ChampionshipDetailsDTO>> GenerateChampionship(ChampionshipEntity championship, IList<Guid> selectedTeams)
        {
            var teams = await _teamRepository.GetByUuidList(selectedTeams);

            var winners = new List<TeamEntity>();

            var quarterfinals = await GenerateMatches(championship, teams, EChampionshipStage.QuarterFinals);

            winners = teams.Where(team => quarterfinals.Any(match => match.ChampionshipStage == EChampionshipStage.QuarterFinals && match.MatchWinnerUuid == team.Uuid)).ToList();

            var semifinals = await GenerateMatches(championship, winners, EChampionshipStage.SemiFinals);

            winners = teams.Where(team => semifinals.Any(match => match.ChampionshipStage == EChampionshipStage.SemiFinals && match.MatchWinnerUuid == team.Uuid)).ToList();

            var semifinalParticipants = semifinals
              .Where(match => match.ChampionshipStage == EChampionshipStage.SemiFinals)
              .SelectMany(match => new[] { match.HomeTeamUuid, match.AwayTeamUuid })
              .ToList();

            var losers = teams
                .Where(team => semifinalParticipants.Contains(team.Uuid) && !winners.Any(winner => winner.Uuid == team.Uuid))
                .ToList();



            var finals = await GenerateMatches(championship, winners, EChampionshipStage.Final);
            var thirdPlace = await GenerateMatches(championship, losers, EChampionshipStage.ThirdPlacePlayoff);

            return (List<ChampionshipDetailsDTO>)await _championshipHistoryRepository.GetHistoryByUuid(championship.Uuid);
        }
        public override async Task<ChampionshipResponse> AddAsync(ChampionshipRequest request)
        {
            var user = await _userRepository.GetByUuidAsync(request.UserUuid);
            var lastChsmpionship = await _repository.GetAllAsync();
            var number = lastChsmpionship.Count() + 1;
            var championshipEntity = new ChampionshipEntity { 
                Name = "Championship #"+number,
                UserId = (int)user.Id
            };

            var addedChampionship = await _repository.AddAsync(championshipEntity);

            var matches = await GenerateChampionship(addedChampionship, request.Teams);

            var response = new ChampionshipResponse {
                Uuid = addedChampionship.Uuid,
                Name = addedChampionship.Name,
                User = _mapper.Map<UserResponse>(user),
                Matches = _mapper.Map<List<MatchResponse>>(matches)
            };
            return response;
        }

        public Task<IList<ChampionshipDetailsDTO>> GetChampionshipDetails(Guid userUuid)
        {
            throw new NotImplementedException();
        }
    }
}

