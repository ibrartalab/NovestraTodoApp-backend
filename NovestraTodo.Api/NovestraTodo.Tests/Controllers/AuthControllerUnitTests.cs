using Moq;
using NovestraTodo.Api.Controllers;
using NovestraTodo.Application.Services.Interfaces;


namespace NovestraTodo.Tests.Controllers
{
    public class AuthControllerUnitTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerUnitTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnOkResult() {
            // Arrange
            var registerRequestDto = new Application.DTOs.RegisterRequestDto
            {
                Username = "testuser",
                Email = "test@gmail.com",
                Password = "Password123!",
                FirstName = "Test",
                LastName = "User",

            };
            var authResponseDto = new Application.DTOs.AuthResponseDto
            {
                Token = "sampleToeken",
                User = new Application.DTOs.UserDto {
                    FirstName = "Test",
                    LastName = "User",
                    UserName = "testuser",
                    Email = "test@gmail.com",
                }
            };

            _mockAuthService.Setup(service => service.RegisterUser(registerRequestDto))
                .ReturnsAsync(authResponseDto);
            // Act
            var result = await _authController.Register(registerRequestDto);
            // Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Application.DTOs.AuthResponseDto>(okResult.Value);
            Assert.Equal(authResponseDto.Token, returnValue.Token);
            Assert.Equal(authResponseDto.User.Email, returnValue.User.Email);
        }
        [Fact]
        public async Task Login_ShouldReturnOkResult() {
            // Arrange
            var loginRequestDto = new Application.DTOs.LoginRequestDto
            {
                UserName = "testuser",
                Password = "Password123!",
            };
            var authResponseDto = new Application.DTOs.AuthResponseDto
            {
                Token = "sampleToeken",
                User = new Application.DTOs.UserDto
                {
                    FirstName = "Test",
                    LastName = "User",
                    UserName = "testuser",
                    Email = "test@gmail.com",
                }
                };
            _mockAuthService.Setup(service => service.LoginUser(loginRequestDto))
                .ReturnsAsync(authResponseDto);
            // Act
            var result = await _authController.Login(loginRequestDto);
            // Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Application.DTOs.AuthResponseDto>(okResult.Value);
            Assert.Equal(authResponseDto.Token, returnValue.Token);
            Assert.Equal(authResponseDto.User.Email, returnValue.User.Email);
        }
    }
}
