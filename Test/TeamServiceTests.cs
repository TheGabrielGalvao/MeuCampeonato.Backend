using AutoMapper;
using Domain.DTO;
using Domain.Entity;
using Domain.Interface.Repository;
using Moq;
using Service;
using Test;

namespace Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private TeamService _teamService;
        private Mock<ITeamRepository> _teamRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _teamRepositoryMock = new Mock<ITeamRepository>();

            _mapper = AutoMapperTestConfiguration.Configure();

            _teamService = new TeamService(_teamRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfTeams()
        {
            var teams = new List<TeamEntity>
                {
                    new TeamEntity {
                        Id = 1,
                        Uuid = Guid.NewGuid(),
                        Name = "Team1"
                        
                    },
                    new TeamEntity {
                        Id = 2,
                        Uuid = Guid.NewGuid(),
                        Name = "Team2"
                    }
                };


            _teamRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(teams);

            var result = await _teamService.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsInstanceOf<IEnumerable<TeamResponse>>(result);
            Assert.AreEqual(teams.Count, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnTeamById()
        {
            var teamId = Guid.NewGuid();
            var team = new TeamEntity
            {
                Id = 1,
                Uuid = teamId,
                Name = "TestTeam"
            };

            _teamRepositoryMock.Setup(repo => repo.GetByUuidAsync(teamId)).ReturnsAsync(team);

            var result = await _teamService.GetByIdAsync(teamId);

            Assert.NotNull(result);
            Assert.IsInstanceOf<TeamResponse>(result);
            Assert.AreEqual(teamId, result.Uuid);
            Assert.AreEqual("TestTeam", result.Name);
        }


        [Test]
        public async Task DeleteAsync_ShouldDeleteTeam()
        {
            var teamId = Guid.NewGuid();
            var existingTeam = new TeamEntity
            {
                Id = 1,
                Uuid = teamId,
                Name = "TeamToDelete"
            };

            _teamRepositoryMock.Setup(repo => repo.GetByUuidAsync(teamId)).ReturnsAsync(existingTeam
        );

            await _teamService.DeleteAsync(teamId);

            _teamRepositoryMock.Verify(repo => repo.DeleteAsync(teamId), Times.Once);
        }

        [Test]
        public async Task AddAsync_ShouldAddNewTeam()
        {
            var teamRequest = new TeamRequest
            {
                Name = "NewTeam"
            };

            var addedTeamId = Guid.NewGuid();
            var addedTeam = new TeamEntity
            {
                Id = 1,
                Uuid = addedTeamId,
                Name = "NewTeam"
            };

            _mapper = AutoMapperTestConfiguration.Configure();
            _teamRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<TeamEntity>()))
                .ReturnsAsync(addedTeam);

            var result = await _teamService.AddAsync(teamRequest);

            Assert.NotNull(result);
            Assert.IsInstanceOf<TeamResponse>(result);
            Assert.AreEqual(addedTeamId, result.Uuid);
            Assert.AreEqual("NewTeam", result.Name);
        }

    }
}
