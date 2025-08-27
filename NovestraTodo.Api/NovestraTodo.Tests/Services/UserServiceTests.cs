using NovestraTodo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly UserService _userService;

        public UserServiceTests() {
            _userRepoMock = new Mock<IUserRepository>();
            _userService = new UserService(_userService.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnUserDto_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { id = userId, name = "Jhone Doe" };
        }
    }
}
