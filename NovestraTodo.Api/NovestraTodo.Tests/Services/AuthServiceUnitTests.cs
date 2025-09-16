using Microsoft.AspNetCore.Identity.Data;
using Moq;
using NovestraTodo.Application.Services.Implementation;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Tests.Services
{
    public class AuthServiceUnitTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly IAuthService _authService;

        public AuthServiceUnitTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtService = new Mock<IJwtService>();
            _authService = new AuthService(_mockUserRepository.Object, _mockJwtService.Object);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnAuthResponseDto()
        {
            //Arrange
            var registerRequest = new Application.DTOs.RegisterRequestDto
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "john@gmail.com",
                Password = "password123"
            };
            var authResponse = new Application.DTOs.AuthResponseDto
            {
                Token = "mocked_jwt_token",
                User = new Application.DTOs.UserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    UserName = registerRequest.Username,
                    Email = registerRequest.Email,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(registerRequest.Username)).ReturnsAsync((Core.Entities.UserEntity?)null);
            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<Core.Entities.UserEntity>())).ReturnsAsync((Core.Entities.UserEntity user) => user);
            _mockJwtService.Setup(jwt => jwt.GenerateToken(It.IsAny<Core.Entities.UserEntity>())).Returns("mocked_jwt_token");
            //Act
            var result = await _authService.RegisterUser(registerRequest);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("mocked_jwt_token", result.Token);
            Assert.Equal(registerRequest.FirstName, result.User.FirstName);
            Assert.Equal(registerRequest.LastName, result.User.LastName);
            Assert.Equal(registerRequest.Username, result.User.UserName);
            Assert.Equal(registerRequest.Email, result.User.Email);
        }
        [Fact]
        public async Task RegisterUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            //Arrange
            var existingUser = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "Existing",
                LastName = "User",
                UserName = "johndoe",
                Email = "john@gmail.com",
                Password = "password123",
                CreatedAt = DateTime.UtcNow
            };
            var registerRequest = new Application.DTOs.RegisterRequestDto
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "john@gmail.com",
                Password = "password123"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(registerRequest.Username)).ReturnsAsync(existingUser);
            //Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _authService.RegisterUser(registerRequest)
            );
            Assert.Equal("User already exist!", ex.Message);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnAuthResponseDto()
        {
            //Arrange
            var loginRequest = new Application.DTOs.LoginRequestDto
            {
                UserName = "johndoe",
                Password = "password123"
            };
            var user = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "john@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                CreatedAt = DateTime.UtcNow
            };
            var authResponse = new Application.DTOs.AuthResponseDto
            {
                Token = "mocked_jwt_token",
                User = new Application.DTOs.UserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = DateTime.UtcNow
                }
            };
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(loginRequest.UserName)).ReturnsAsync(user);
            _mockJwtService.Setup(jwt => jwt.GenerateToken(It.IsAny<Core.Entities.UserEntity>())).Returns("mocked_jwt_token");
            //Act
            var result = await _authService.LoginUser(loginRequest);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("mocked_jwt_token", result.Token);
            Assert.Equal(user.FirstName, result.User.FirstName);
            Assert.Equal(user.LastName, result.User.LastName);
            Assert.Equal(user.UserName, result.User.UserName);
            Assert.Equal(user.Email, result.User.Email);
            Assert.Equal(true, BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password));
        }

        [Fact]
        public async Task LoginUser_ShouldThrowUnauthorizedAccessException_WhenCredentialsAreInvalid()
        {
            //Arrange
            var loginRequest = new Application.DTOs.LoginRequestDto
            {
                UserName = "johndoe",
                Password = "wrongpassword"
            };
            var user = new Core.Entities.UserEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "john@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password123"),
                CreatedAt = DateTime.UtcNow
            };
            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(loginRequest.UserName)).ReturnsAsync(user);
            //Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _authService.LoginUser(loginRequest)
            );
            Assert.Equal("Invalid Credentials", ex.Message);
        }
    }
}
