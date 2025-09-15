using Moq;
using NovestraTodo.Api.Controllers;
using NovestraTodo.Application.Services.Interfaces;

namespace NovestraTodo.Tests.Controllers
{
    public class UserControllerUnitTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockUserService;

        public UserControllerUnitTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);

        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult()
        {
            // Arrange
            var users = new List<Application.DTOs.UserDto>
            {
                new Application.DTOs.UserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Test1",
                    LastName = "User1",
                    UserName = "testuser1",
                    Email = "test@gmail.com",
                },
                new Application.DTOs.UserDto
                {
                    Id = Guid.NewGuid(),
                    FirstName="Test2",
                    LastName="User2",
                    UserName="testuser2",
                    Email="testsuer2@gmail.com"
                }
            };

            _mockUserService.Setup(service => service.GetUsers())
                .ReturnsAsync(users.Select(u => new Core.Entities.UserEntity
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    Password = "HashedPassword" // Simulate hashed password
                }));
            // Act
            var result = await _userController.GetAll();
            // Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Application.DTOs.UserDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }
        [Fact]
        public async Task GetByUsername_ShouldReturnOkResult()
        {
            // Arrange
            var userName = "testuser1";
            var user = new Application.DTOs.UserDto
            {
                Id = Guid.NewGuid(),
                FirstName = "Test1",
                LastName = "User1",
                UserName = "testuser1",
                Email = "test@gmail.com"

            };
            _mockUserService.Setup(service => service.GetUserByUsername(userName))
                .ReturnsAsync(new Core.Entities.UserEntity
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = "HashedPassword" // Simulate hashed password
                });
            var result = await _userController.GetByUsername(userName);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Application.DTOs.UserDto>(okResult.Value);
            Assert.Equal(userName, returnValue.UserName);
        }
        [Fact]
        public async Task Update_ShouldReturnOkResult()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var existingUser = new Core.Entities.UserEntity
            {
                Id = userId,
                FirstName = "test",
                LastName = "user",
                UserName = "testuser",
                Email = "testuser@gmail.com",
                Password = "HashedPassowrd"
            };
            var updatedUser = new Core.Entities.UserEntity
            {
                Id = existingUser.Id,
                FirstName = "test1",
                LastName = "user1",
                UserName = "testuser11",
                Email = "testuser11@gmail.com",
                Password = "HashedPassowrd"
            };
            _mockUserService.Setup(service => service.UpdateUser(userId, updatedUser)).ReturnsAsync(updatedUser);
            //Act
            var result = await _userController.Update(userId, updatedUser);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Application.DTOs.UserDto>(okResult.Value);
            Assert.Equal($"{userId}", returnValue.UserName);
        }
        [Fact]
        public async Task Delete_ShouldReturnTrue()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var deletedUser = new Core.Entities.UserEntity
            {
                Id = userId,
                FirstName = "test1",
                LastName = "user1",
                UserName = "testuser11",
                Email = "testuser11@gmail.com",
                Password = "HashedPassowrd"
            };
            _mockUserService.Setup(service => service.DeleteUser(userId)).ReturnsAsync(true);
            //Act
            var result = await _userController.Delete(userId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Application.DTOs.UserDto>(okResult.Value);

            Assert.Equal(returnValue.Id, userId);
        }
    }
}
