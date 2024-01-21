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
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            _mapper = AutoMapperTestConfiguration.Configure();

            _userService = new UserService(_userRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfUsers()
        {
            var users = new List<UserEntity>
            {
                new UserEntity {
                    Id = 1,
                    Uuid = Guid.NewGuid(),
                    UserName = "User1",
                    UserPass = "$2a$11$z0y.yWAl.MR/mIOB5laEieBL47hpZscYZ9bvg.z.dtu82nEbgCTJu"
                },
                new UserEntity {
                    Id = 2,
                    Uuid = Guid.NewGuid(),
                    UserName = "User2",
                    UserPass = "$2a$11$z0y.yWAl.MR/mIOB5laEieBL47hpZscYZ9bvg.z.dtu82nEbgCTJu"
                }
            };


            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

            var result = await _userService.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsInstanceOf<IEnumerable<UserResponse>>(result);
            Assert.AreEqual(users.Count, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnUserById()
        {
            var userId = Guid.NewGuid();
            var user = new UserEntity
            {
                Id = 1,
                Uuid = userId,
                UserName = "TestUser",
                UserPass = "$2a$11$z0y.yWAl.MR/mIOB5laEieBL47hpZscYZ9bvg.z.dtu82nEbgCTJu"
            };

            _userRepositoryMock.Setup(repo => repo.GetByUuidAsync(userId)).ReturnsAsync(user);

            var result = await _userService.GetByIdAsync(userId);

            Assert.NotNull(result);
            Assert.IsInstanceOf<UserResponse>(result);
            Assert.AreEqual(userId, result.Uuid);
            Assert.AreEqual("TestUser", result.UserName);
        }

        [Test]
        public async Task AddAsync_ShouldHashUserPassword()
        {
            var userRequest = new UserRequest
            {
                UserName = "TestUser",
                UserPass = "1234"
            };

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(new UserEntity
                {
                    Id = 1,
                    Uuid = Guid.NewGuid(),
                    UserName = "TestUser",
                    UserPass = "$2a$11$z0y.yWAl.MR/mIOB5laEieBL47hpZscYZ9bvg.z.dtu82nEbgCTJu"
                }
            );

            var result = await _userService.AddAsync(userRequest);

            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<UserEntity>()), Times.Once);

            Assert.NotNull(result);
            Assert.AreNotEqual("1234", result.UserPass);
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteUser()
        {
            var userId = Guid.NewGuid();
            var existingUser = new UserEntity
            {
                Id = 1,
                Uuid = userId,
                UserName = "UserToDelete",
                UserPass = "$2a$11$z0y.yWAl.MR/mIOB5laEieBL47hpZscYZ9bvg.z.dtu82nEbgCTJu"
            };

            _userRepositoryMock.Setup(repo => repo.GetByUuidAsync(userId)).ReturnsAsync(existingUser
        );

            await _userService.DeleteAsync(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }

    }
}
