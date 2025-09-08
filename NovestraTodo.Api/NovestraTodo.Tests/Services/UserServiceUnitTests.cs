using Moq;
using NovestraTodo.Application.Services.Implementation;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Tests.Services
{
    public class UserServiceUnitTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly IUserService _userService;

        public UserServiceUnitTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnAllUsers()
        {
            //Arrange
            var users = new List<Core.Entities.UserEntity>
            {
                new() {
                    Id=Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "johndoe",
                    Email = "jhone@gmail.com",
                    Password = "password123",
                    CreatedAt = DateTime.UtcNow
                },
                new () {
                    Id=Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    UserName = "janesmith",
                    Email = "smith@gmail.com",
                    Password = "password456",
                    CreatedAt = DateTime.UtcNow
            }
                };
            _mockUserRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);
            //Act
            var result = await _userService.GetUsers();
            //Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("johndoe", result.First().UserName);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnSpecificUserById()
        {

        }
    }
}
