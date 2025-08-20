using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        //private readonly ITodoService _todoService;

        //public TodoController(ITodoService todoService)
        //{
        //    _todoService = todoService;
        //}

        // Get all todo items from this endpoint 
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<TodoEntity>> GetAll()
        {
            var result = await todoService.GetTodos();

            return Ok(result);
        }

        // Get all todos for a specific user
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<TodoEntity>> GetUserAllTodos(Guid userId)
        {
            var result = await todoService.GetUserTodos(userId);
            return Ok(result);
        }
        // Add a todo item from this endpoint
        [Authorize]
        [HttpPost("")]
        public async Task<ActionResult<TodoEntity>> Add(TodoEntity Todo)
        {
            var result = await todoService.AddTodo(Todo);

            return Ok(result);
        }

        // Update todo item from this endpoint
        [Authorize]
        [HttpPut("{todoId}")]
        public async Task<ActionResult<TodoEntity>> Upddate([FromRoute]int todoId,[FromBody]TodoEntity Todo)
        {
            var result = await todoService.UpdateTodo(todoId,Todo);

            return Ok(result);
        }

        // Delete todo item from this endpoint
        [Authorize]
        [HttpDelete("{todoId}")]
        public async Task<ActionResult<bool>> DeleteTodo(int Id)
        {
            var result = await todoService.DeleteTodo(Id);

            return Ok(result);
        }

        
    }

}
