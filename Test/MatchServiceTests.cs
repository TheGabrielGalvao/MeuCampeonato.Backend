using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface.Repository;
using Moq;
using Service;
using System;
using Test;

namespace Tests
{
    [TestFixture]
    public class MatchServiceTests
    {
        private MatchService _matchService;
        private Mock<IMatchRepository> _matchRepositoryMock;
        private Mock<IChampionshipHistoryRepository> _championshipHistoryRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _matchRepositoryMock = new Mock<IMatchRepository>();
            _championshipHistoryRepositoryMock = new Mock<IChampionshipHistoryRepository>();
            _mapper = AutoMapperTestConfiguration.Configure();
            _matchService = new MatchService(_matchRepositoryMock.Object, _championshipHistoryRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GenerateResultMatch_ShouldGenerateValidResult()
        {
            var random = new Random();

            var match = new MatchEntity
            {
                Id = random.Next(1, 100),
                Uuid = Guid.NewGuid(),
                HomeTeamId = random.Next(1, 100),
                AwayTeamId = random.Next(1, 100),
                HomeTeamNormalTimeScore = random.Next(0, 8),
                AwayTeamNormalTimeScore = random.Next(0, 8),
                HomeTeamPenaltyScore = random.Next(0, 15),
                AwayTeamPenaltyScore = random.Next(0, 15),
                MatchWinnerId = random.Next(1, 100),
                ChampionshipStage = EChampionshipStage.QuarterFinals,
                ChampionshipId = random.Next(1, 100),

            };


            await _matchService.GenerateResultMatch(match);


            Assert.IsTrue(match.HomeTeamNormalTimeScore >= 0 && match.HomeTeamNormalTimeScore <= 10);
            Assert.IsTrue(match.AwayTeamNormalTimeScore >= 0 && match.AwayTeamNormalTimeScore <= 10);

            if (match.HomeTeamNormalTimeScore == match.AwayTeamNormalTimeScore)
            {
                Assert.IsTrue(match.HomeTeamPenaltyScore >= 0 && match.HomeTeamPenaltyScore <= 5);
                Assert.IsTrue(match.AwayTeamPenaltyScore >= 0 && match.AwayTeamPenaltyScore <= 5);
            }


        }

        [Test]
        public async Task GenerateMatches_ShouldGenerateValidMatches()
        {

            var championship = new ChampionshipEntity { Id = 1, Uuid = Guid.NewGuid(), Name = "TestChampionship", UserId = 1 };
            var selectedTeams = new List<TeamEntity>
            {
                new TeamEntity { Id = 1, Uuid = Guid.NewGuid(), Name = "Team1" },
                new TeamEntity { Id = 2, Uuid = Guid.NewGuid(), Name = "Team2" },
                new TeamEntity { Id = 3, Uuid = Guid.NewGuid(), Name = "Team3" },
                new TeamEntity { Id = 4, Uuid = Guid.NewGuid(), Name = "Team4" },
                new TeamEntity { Id = 5, Uuid = Guid.NewGuid(), Name = "Team5" },
                new TeamEntity { Id = 6, Uuid = Guid.NewGuid(), Name = "Team6" },
                new TeamEntity { Id = 7, Uuid = Guid.NewGuid(), Name = "Team7" },
                new TeamEntity { Id = 8, Uuid = Guid.NewGuid(), Name = "Team8" },


            };
            var stage = EChampionshipStage.QuarterFinals;

            _matchRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<MatchEntity>()))
                .ReturnsAsync((MatchEntity match) => match);


            var result = await _matchService.GenerateMatches(championship, selectedTeams, stage);


            Assert.NotNull(result);
            Assert.IsInstanceOf<List<ChampionshipDetailsDTO>>(result);


        }
    }
}
