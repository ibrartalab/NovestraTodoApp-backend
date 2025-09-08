using Microsoft.EntityFrameworkCore;
using NovestraTodo.Infrastructure.Data;
using NovestraTodo.Infrastructure.Repositories;


namespace NovestraTodo.Tests.Repositories
{
    public class TodoRepositoryTests
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
            dbContext.Todos.AddRange(
                new() { Id = 1, Todo = "Test Todo 1", IsCompleted = false, IsRemoved = false },
                new() { Id = 2, Todo = "Test Todo 2", IsCompleted = true, IsRemoved = false }
            );
            // Save changes to the in-memory database
            dbContext.SaveChanges();
            return dbContext;
        }

        // Write tests for the TodoRepository methods
        [Fact]
        public async Task GetAllTodos_ShouldReturnAllTodos()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new TodoRepository(dbContext);
            // Act
            var todos = await repository.GetAllAsync();
            // Assert
            Assert.Equal(2, todos.Count());
            Assert.Contains(todos, t => t.Todo == "Test Todo 1");
            Assert.Contains(todos, t => t.Todo == "Test Todo 2");
        }
        [Fact]
        public async Task AddTodo_ShouldAddNewTodo()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new TodoRepository(dbContext);
            var newTodo = new Core.Entities.TodoEntity { Id = 3, Todo = "New Todo", IsCompleted = false, IsRemoved = false };
            // Act
            var addedTodo = await repository.AddAsync(newTodo);
            var todos = await repository.GetAllAsync();
            // Assert
            Assert.Equal(3, todos.Count());
            Assert.Contains(todos, t => t.Todo == "New Todo");
            Assert.Equal(addedTodo.Todo, newTodo.Todo);
        }
        [Fact]
        public async Task GetByUserId_ShouldReturnUserSpecificTodos()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new TodoRepository(dbContext);
            var userId = Guid.NewGuid();
            dbContext.Todos.AddRange(
                new() { Id = 3, Todo = "User Todo 1", UserId = userId, IsCompleted = false, IsRemoved = false },
                new() { Id = 4, Todo = "User Todo 2", UserId = userId, IsCompleted = true, IsRemoved = false }
            );
            dbContext.SaveChanges();
            // Act
            var userTodos = await repository.GetByUserIdAsync(userId);
            // Assert
            Assert.Equal(2, userTodos.Count());
            Assert.All(userTodos, t => Assert.Equal(userId, t.UserId));
        }
        [Fact]
        public async Task UpdateTodo_ShouldUpdateExistingTodo()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new TodoRepository(dbContext);
            int todoId = 1;
            var todoToUpdate = await repository.GetByIdAsync(1);
            todoToUpdate.Todo = "Updated Todo";
            todoToUpdate.IsCompleted = true;
            // Act
            var updatedTodo = await repository.UpdateAsync(todoId,todoToUpdate);
            // Assert
            Assert.Equal("Updated Todo", updatedTodo.Todo);
            Assert.True(updatedTodo.IsCompleted);
        }
        [Fact]
        public async Task DeleteTodo_ShouldRemoveTodo()
        {
            // Arrange
            using var dbContext = GetInMemoryDbContext();
            var repository = new TodoRepository(dbContext);
            // Act
            await repository.DeleteAsync(1);
            var todos = await repository.GetAllAsync();
            // Assert
            Assert.Single(todos);
            Assert.DoesNotContain(todos, t => t.Id == 1);
        }
    }
}
