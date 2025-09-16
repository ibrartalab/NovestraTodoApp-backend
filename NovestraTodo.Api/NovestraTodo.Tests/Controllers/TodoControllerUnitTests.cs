using Moq;
using NovestraTodo.Api.Controllers;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;


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
            var todos = new List<Application.DTOs.TodoDto>
            {
                new() { Id = 1, Todo = "Test Todo 1", IsCompleted = false,IsRemoved=false },
                new () { Id = 2, Todo = "Test Todo 2", IsCompleted = true,IsRemoved=false }
            };
            _mockTodoService.Setup(service => service.GetTodos()).ReturnsAsync(todos);
            //Act
            var result = await _controller.GetAll();
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TodoDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }
        [Fact]
        public async Task GetUserAllTodos_ShouldReturnUserSpecificTodos()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var todos = new List<Application.DTOs.TodoDto>
            {
                new() { Id = 1, Todo = "User Todo 1", UserId = userId, IsCompleted = false,IsRemoved=false },
                new () { Id = 2, Todo = "User Todo 2", UserId = userId, IsCompleted = true,IsRemoved=false }
            };
            _mockTodoService.Setup(service => service.GetUserTodos(userId)).ReturnsAsync(todos);
            //Act
            var result = await _controller.GetUserAllTodos(userId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TodoDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
            Assert.All(returnValue, t => Assert.Equal(userId, t.UserId));
        }
        [Fact]
        public async Task Add_ShouldReturnAddedTodo()
        {
            //Arrange
            var newTodo = new TodoEntity { Id = 1, Todo = "New Todo", IsCompleted = false,IsRemoved=false ,UserId=Guid.NewGuid()};
            var newTodoDto = new TodoDto { Id = 1, Todo = "New Todo", IsCompleted = false,IsRemoved=false,UserId=newTodo.UserId };
            
            _mockTodoService.Setup(service => service.AddTodo(newTodo)).ReturnsAsync(newTodoDto);
            //Act
            var result = await _controller.Add(newTodo);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TodoDto>(okResult.Value);
            Assert.Equal("New Todo", returnValue.Todo);
            Assert.False(returnValue.IsCompleted);
            Assert.Equal(newTodo.UserId, returnValue.UserId);

        }
        [Fact]
        public async Task Update_ShouldReturnUpdatedTodo()
        {
            //Arrange
            var todoId = 1;
            var updatedTodo = new TodoEntity { Id = todoId, Todo = "Updated Todo", IsCompleted = true,IsRemoved=false,UserId=Guid.NewGuid() };
            var updatedTodoDto = new TodoDto { Id = todoId, Todo = "Updated Todo", IsCompleted = true,IsRemoved=false,UserId=updatedTodo.UserId };

            _mockTodoService.Setup(service => service.UpdateTodo(todoId, updatedTodo)).ReturnsAsync(updatedTodoDto);
            //Act
            var result = await _controller.Upddate(todoId, updatedTodo);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TodoDto>(okResult.Value);
            Assert.Equal("Updated Todo", returnValue.Todo);
            Assert.True(returnValue.IsCompleted);
            Assert.Equal(updatedTodo.UserId, returnValue.UserId);
        }
        [Fact]
        public async Task Delete_ShouldReturnTrue()
        {
            //Arrange
            int todoId = 1;
            var deletedTodo = new TodoEntity { Id = todoId, Todo = "Deleted Todo", IsCompleted = false,IsRemoved=true };
            _mockTodoService.Setup(service => service.DeleteTodo(todoId)).ReturnsAsync(true);
            //Act
            var result = await _controller.Delete(todoId);
            //Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnValue);
        }
    }
}
