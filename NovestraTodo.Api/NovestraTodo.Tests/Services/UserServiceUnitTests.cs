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
        public async Task GetUserById_ShouldReturnSpecificUser() { 
            //Arrange
            var userId = Guid.NewGuid();
            var user = new Core.Entities.UserEntity
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "jhone@gmail.com",
                Password = "password123",
                CreatedAt = DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);

            //Act
            var result = await _userService.GetUserById(userId);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }
        [Fact]
        public async Task GetUserByUsername_ShouldReturnSpecificUser()
        {
            //Arrange
            string userName = "johndoe";
            var user = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "jhone@gmail.com",
                Password = "password123",
                CreatedAt = DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(userName)).ReturnsAsync(user);
            //Act
            var result = await _userService.GetUserByUsername(userName);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(userName, result.UserName);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByInvalidUserName_ShouldReturnNull()
        {
            //Arrange
            string userName = "holland";
            var user = (Core.Entities.UserEntity?)null;
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(userName)).ReturnsAsync(user);
            //Act
            var result = await _userService.GetUserByUsername(userName);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddUser_ShouldReturnAddedUser()
        {
            //Arrange
            var newUser = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Johnson",
                UserName = "alicejohnson",
                Email = "alice@gmail.com",
                Password = "password789",
                CreatedAt = DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.AddUserAsync(newUser)).ReturnsAsync(newUser);
            //Act
            var result = await _userService.AddNewUser(newUser);
            //Assert
            Assert.Equal(newUser.UserName, result.UserName);
            Assert.Equal(newUser.Email, result.Email);
        }
        [Fact]
        public async Task AddUserWithExistingOne_ShouldReturnNull()
        {
            //Arrange
            var existingUser = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Johnson",
                UserName = "alicejohnson",
                Email = "alice@gmail.com",
                Password = "password789",
                CreatedAt = DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.AddUserAsync(existingUser)).ReturnsAsync((Core.Entities.UserEntity?)null);
            //Act
            var result = await _userService.AddNewUser(existingUser);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task UpdateUser_ShouldReturnUpdatedUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var existingUser = new Core.Entities.UserEntity
            {
                Id = userId,
                FirstName = "Alice",
                LastName = "Johnson",
                UserName = "alicejohnson",
                Email = "alice@gmail.com",
                Password = "password78910",
                CreatedAt = DateTime.UtcNow
            };
            var updatedUser = new Core.Entities.UserEntity
            {
                Id = existingUser.Id,
                FirstName = "Alice2",
                LastName = "Johnson",
                UserName = "alicejohnson2",
                Email = "alice@gmail.com",
                Password = "password78910",
                CreatedAt = DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.UpdateUserAsync(existingUser.Id, updatedUser)).ReturnsAsync(updatedUser);
            //Act
            var result = await _userService.UpdateUser(existingUser.Id, updatedUser);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Alice2", result.FirstName);
            Assert.Equal("alicejohnson2", result.UserName);
        }
        [Fact]
        public async Task UpdateUserWithInvalidId_ShouldReturnNull()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var existingUser = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Johnson",
                UserName = "alicejohnson",
                Email = "alice@gmail.com",
                Password = "12334alice",
                CreatedAt = DateTime.UtcNow
            };
            var updatedUser = new Core.Entities.UserEntity
            {
                Id = userId,
                FirstName = "Updated FirstName",
                LastName = "Updated LastName",
                UserName = "alicejohnson",
                Email = "alice@gmail.com",
                Password="12334alice",
                CreatedAt= DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.UpdateUserAsync(userId, updatedUser)).ReturnsAsync((Core.Entities.UserEntity?)null);
            //Act
            var result = await _userService.UpdateUser(userId, updatedUser);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteUser_ShouldReturnTrue()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(repo => repo.DeleteUserAsync(userId)).ReturnsAsync(true);
            //Act
            var result = await _userService.DeleteUser(userId);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteUserWithInvalidId_ShouldReturnFalse()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(repo => repo.DeleteUserAsync(userId)).ReturnsAsync(false);
            //Act
            var result = await _userService.DeleteUser(userId);
            //Assert
            Assert.False(result);
        }
    }
}
