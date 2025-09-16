using Microsoft.EntityFrameworkCore;
using NovestraTodo.Infrastructure.Data;


namespace NovestraTodo.Tests.Repositories
{
    public class UserRepositoryTests
    {
        // This is a private method to create an in-memory database context for testing
        private NovestraDbContext GetInMemoryDbContext()
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<NovestraDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            // Create instance of DbContext
            var dbContext = new NovestraDbContext(options);
            // Ensure the database is created
            dbContext.Database.EnsureCreated();

            // Seed the database with test data
            dbContext.Users.AddRange(
                new() { Id = Guid.NewGuid(), FirstName = "Test1", LastName = "User1", UserName = "testuser1", Email = "testuser1@gmail.com", Password = "password123", CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), FirstName = "Test2", LastName = "User2", UserName = "testuser2", Email = "testuser2@gmail.com", Password = "password456", CreatedAt = DateTime.UtcNow }
            );
            // Save changes to the in-memory database
            dbContext.SaveChanges();
            return dbContext;
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            // Act
            var users = await repository.GetAllUsersAsync();
            // Assert
            Assert.Equal(2, users.Count());
            Assert.Contains(users, u => u.UserName == "testuser1");
            Assert.Contains(users, u => u.UserName == "testuser2");
        }
        [Fact]
        public async Task AddUser_ShouldAddNewUser()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            var newUser = new Core.Entities.UserEntity { FirstName = "Test3", LastName = "User3", UserName = "testuser3", Email = "testuser3@gmail.com", Password = "password789", CreatedAt = DateTime.UtcNow };
            // Act
            var addedUser = await repository.AddUserAsync(newUser);
            var users = await repository.GetAllUsersAsync();
            // Assert
            Assert.Equal(3, users.Count());
            Assert.Contains(users, u => u.UserName == "testuser3");
            Assert.Equal(addedUser.UserName, newUser.UserName);
        }
        [Fact]
        public async Task GetUserByUsername_ShouldReturnSpecificUser()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            var username = "testuser1";
            // Act
            var user = await repository.GetUserByUsernameAsync(username);
            // Assert
            Assert.NotNull(user);
            Assert.Equal("testuser1", user?.UserName);
        }
        [Fact]
        public async Task UpdateUser_ShouldUpdateExistingUser()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            var existingUser = dbContext.Users.First();
            var updatedUser = new Core.Entities.UserEntity
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Password = "UpdatedPassword",
                UserName = existingUser.UserName,
            };
            // Act
            var result = await repository.UpdateUserAsync(existingUser.Id, updatedUser);
            var user = await repository.GetUserByIdAsync(existingUser.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedFirstName", user?.FirstName);
            Assert.Equal("UpdatedLastName", user?.LastName);
            Assert.Equal("UpdatedPassword", user?.Password);
        }
        [Fact]
        public async Task UpdateUser_NonExistentUser_ShouldReturnNull()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            var nonExistentUserId = Guid.NewGuid();
            var updatedUser = new Core.Entities.UserEntity
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Password = "UpdatedPassword",
                UserName = "nonexistentuser",
            };
            // Act
            var result = await repository.UpdateUserAsync(nonExistentUserId, updatedUser);
            var users = await repository.GetAllUsersAsync();
            // Assert
            Assert.Null(result);
            Assert.Equal(2, users.Count());
        }
        [Fact]
        public async Task DeleteUser_ShouldRemoveUser()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            var existingUser = dbContext.Users.First();
            // Act
            var result = await repository.DeleteUserAsync(existingUser.Id);
            var user = await repository.GetUserByIdAsync(existingUser.Id);
            var users = await repository.GetAllUsersAsync();
            // Assert
            Assert.True(result);
            Assert.Null(user);
            Assert.Single(users);
        }
        [Fact]
        public async Task DeleteUser_NonExistentUser_ShouldReturnFalse()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new NovestraTodo.Infrastructure.Repositories.UserRepository(dbContext);
            var nonExistentUserId = Guid.NewGuid();
            // Act
            var result = await repository.DeleteUserAsync(nonExistentUserId);
            var users = await repository.GetAllUsersAsync();
            // Assert
            Assert.False(result);
            Assert.Equal(2, users.Count());
        }
    }
}
