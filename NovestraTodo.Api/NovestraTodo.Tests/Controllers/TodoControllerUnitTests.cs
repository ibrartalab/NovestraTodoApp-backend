using Moq;
using NovestraTodo.Api.Controllers;
using NovestraTodo.Application.Services.Interfaces;


namespace NovestraTodo.Tests.Controllers
{
    public class TodoControllerUnitTests
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodoController _controller;
        
        public TodoControllerUnitTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            _controller = new TodoController(_mockTodoService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTodos()
        {
            //Arrange
            var todos = new List<Core.Entities.TodoEntity>
            {
                new() { Id = 1, Todo = "Test Todo 1", IsCompleted = false,IsRemoved=false },
                new () { Id = 2, Todo = "Test Todo 2", IsCompleted = true,IsRemoved=false }
            };
            _mockTodoService.Setup(service => service.GetTodos()).ReturnsAsync(todos);
            //Act
            var result = await _controller.GetAll();
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Core.Entities.TodoEntity>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }
        [Fact]
        public async Task GetUserAllTodos_ShouldReturnUserSpecificTodos()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var todos = new List<Core.Entities.TodoEntity>
            {
                new() { Id = 1, Todo = "User Todo 1", UserId = userId, IsCompleted = false,IsRemoved=false },
                new () { Id = 2, Todo = "User Todo 2", UserId = userId, IsCompleted = true,IsRemoved=false }
            };
            _mockTodoService.Setup(service => service.GetUserTodos(userId)).ReturnsAsync(todos);
            //Act
            var result = await _controller.GetUserAllTodos(userId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Core.Entities.TodoEntity>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
            Assert.All(returnValue, t => Assert.Equal(userId, t.UserId));
        }
        [Fact]
        public async Task Add_ShouldReturnAddedTodo()
        {
            //Arrange
            var newTodo = new Core.Entities.TodoEntity { Id = 1, Todo = "New Todo", IsCompleted = false,IsRemoved=false };
            _mockTodoService.Setup(service => service.AddTodo(newTodo)).ReturnsAsync(newTodo);
            //Act
            var result = await _controller.Add(newTodo);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Core.Entities.TodoEntity>(okResult.Value);
            Assert.Equal("New Todo", returnValue.Todo);
        }
        [Fact]
        public async Task Update_ShouldReturnUpdatedTodo()
        {
            //Arrange
            var todoId = 1;
            var updatedTodo = new Core.Entities.TodoEntity { Id = todoId, Todo = "Updated Todo", IsCompleted = true,IsRemoved=false };
            _mockTodoService.Setup(service => service.UpdateTodo(todoId, updatedTodo)).ReturnsAsync(updatedTodo);
            //Act
            var result = await _controller.Upddate(todoId, updatedTodo);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Core.Entities.TodoEntity>(okResult.Value);
            Assert.Equal("Updated Todo", returnValue.Todo);
            Assert.True(returnValue.IsCompleted);
        }
        [Fact]
        public async Task Delete_ShouldReturnDeletedTodo()
        {
            //Arrange
            var todoId = 1;
            var deletedTodo = new Core.Entities.TodoEntity { Id = todoId, Todo = "Deleted Todo", IsCompleted = false,IsRemoved=true };
            _mockTodoService.Setup(service => service.DeleteTodo(todoId)).ReturnsAsync(deletedTodo);
            //Act
            var result = await _controller.Delete(todoId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Core.Entities.TodoEntity>(okResult.Value);
            Assert.Equal("Deleted Todo", returnValue.Todo);
            Assert.True(returnValue.IsRemoved);
        }
    }
}
