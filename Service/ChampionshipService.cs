using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using System;

namespace Service
{
    public class ChampionshipService : BaseService<ChampionshipEntity, ChampionshipRequest, ChampionshipResponse>, IChampionshipService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchService _matchService;
        private readonly IChampionshipHistoryRepository _championshipHistoryRepository;

        public ChampionshipService(IChampionshipRepository repository, IUserRepository userRepository, ITeamRepository teamRepository, IMatchService matchService, IChampionshipHistoryRepository championshipHistoryRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _matchService = matchService;
            _championshipHistoryRepository = championshipHistoryRepository;
        }

        
        public async Task<List<ChampionshipDetailsDTO>> GenerateChampionship(ChampionshipEntity championship, IList<Guid> selectedTeams)
        {
            var teams = await _teamRepository.GetByUuidList(selectedTeams);

            var winners = new List<TeamEntity>();

            var quarterfinals = await _matchService.GenerateMatches(championship, teams, EChampionshipStage.QuarterFinals);

            winners = teams.Where(team => quarterfinals.Any(match => match.ChampionshipStage == EChampionshipStage.QuarterFinals && match.MatchWinnerUuid == team.Uuid)).ToList();

            var semifinals = await _matchService.GenerateMatches(championship, winners, EChampionshipStage.SemiFinals);

            winners = teams.Where(team => semifinals.Any(match => match.ChampionshipStage == EChampionshipStage.SemiFinals && match.MatchWinnerUuid == team.Uuid)).ToList();

            var semifinalParticipants = semifinals
              .Where(match => match.ChampionshipStage == EChampionshipStage.SemiFinals)
              .SelectMany(match => new[] { match.HomeTeamUuid, match.AwayTeamUuid })
              .ToList();

            var losers = teams
                .Where(team => semifinalParticipants.Contains(team.Uuid) && !winners.Any(winner => winner.Uuid == team.Uuid))
                .ToList();



            var finals = await _matchService.GenerateMatches(championship, winners, EChampionshipStage.Final);
            var thirdPlace = await _matchService.GenerateMatches(championship, losers, EChampionshipStage.ThirdPlacePlayoff);

            var response = await GetChampionshipDetailsByUuid(championship.Uuid);

            return _mapper.Map<List<ChampionshipDetailsDTO>>(response);
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

        public async Task<IList<ChampionshipDetailsDTO>> GetChampionshipDetailsByUuid(Guid uuid)
        {
            var response = await _championshipHistoryRepository.GetHistoryByUuid(uuid);

            return _mapper.Map<List<ChampionshipDetailsDTO>>(response);
        }

        public async Task<IList<ChampionshipDetailsDTO>> GetChampionshipDetailsByUserUuid(Guid userUuid)
        {
            var response = await _championshipHistoryRepository.GetHistoryByUserUuid(userUuid);

            return _mapper.Map<List<ChampionshipDetailsDTO>>(response);
        }
    }
}

