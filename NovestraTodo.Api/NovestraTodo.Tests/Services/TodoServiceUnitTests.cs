using Moq;
using NovestraTodo.Application.Services.Implementation;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Tests.Services
{
    public class TodoServiceUnitTests
    {
        private readonly Mock<ITodoRepository> _mockTodoRepository;
        private readonly ITodoService _todoService;

        public TodoServiceUnitTests()
        {
            _mockTodoRepository = new Mock<ITodoRepository>();
             _todoService = new TodoService(_mockTodoRepository.Object);
        }

        [Fact]
        public async Task GetTodos_ShouldReturnAllTodos()
        {
            //Arrange
            var todos = new List<Core.Entities.TodoEntity>
            {
                new() { Id = 1, Todo = "Test Todo 1", IsCompleted = false,IsRemoved=false },
                new () { Id = 2, Todo = "Test Todo 2", IsCompleted = true,IsRemoved=false }
            };
            _mockTodoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);
            //Act
            var result = await _todoService.GetTodos();
            //Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Test Todo 1", result.First().Todo);
        }

        [Fact]
        public async Task GetUserTodos_ShouldReturnUserSpecificTodos()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var todos = new List<Core.Entities.TodoEntity>
            {
                new() { Id = 1, Todo = "User Todo 1", UserId = userId, IsCompleted = false,IsRemoved=false },
                new () { Id = 2, Todo = "User Todo 2", UserId = userId, IsCompleted = true,IsRemoved=false }
            };
            _mockTodoRepository.Setup(repo => repo.GetByUserIdAsync(userId)).ReturnsAsync(todos);
            //Act
            var result = await _todoService.GetUserTodos(userId);
            //Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.Equal(userId, t.UserId));
        }

        [Fact]
        public async Task AddTodo_ShouldReturnAddedTodo()
        {
            //Arrange
            var newTodo = new Core.Entities.TodoEntity { Id = 1, Todo = "New Todo", IsCompleted = false,IsRemoved=false };
            _mockTodoRepository.Setup(repo => repo.AddAsync(newTodo)).ReturnsAsync(newTodo);
            //Act
            var result = await _todoService.AddTodo(newTodo);
            //Assert
            Assert.Equal("New Todo", result.Todo);
            Assert.False(result.IsCompleted);
        }

        [Fact]
        public async Task UpdateTodo_ShouldReturnUpdatedTodo()
        {
            //Arrange
            var existingTodo = new Core.Entities.TodoEntity { Id = 1, Todo = "Existing Todo", IsCompleted = false,IsRemoved=false };
            var updatedTodo = new Core.Entities.TodoEntity { Id = 1, Todo = "Updated Todo", IsCompleted = true,IsRemoved=false };
            _mockTodoRepository.Setup(repo => repo.UpdateAsync(existingTodo.Id, updatedTodo)).ReturnsAsync(updatedTodo);
            //Act
            var result = await _todoService.UpdateTodo(existingTodo.Id, updatedTodo);
            //Assert
            Assert.Equal("Updated Todo", result.Todo);
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task DeleteTodo_ShouldReturnTrueIfDeleted()
        {
            //Arrange
            var todoId = 1;
            _mockTodoRepository.Setup(repo => repo.DeleteAsync(todoId)).ReturnsAsync(true);
            //Act
            var result = await _todoService.DeleteTodo(todoId);
            //Assert
            Assert.True(result);
        }

    }
}
