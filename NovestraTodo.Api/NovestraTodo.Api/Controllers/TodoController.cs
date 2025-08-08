using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.Commands;
using NovestraTodo.Application.Commands.Todo;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Queries;
using NovestraTodo.Application.Queries.Todo;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ISender sender) : ControllerBase
    {
        // Add a todo item from this endpoint
        [Authorize]
        [HttpPost("")]
        public async Task<ActionResult<TodoEntity>> AddTodo(TodoEntity Todo)
        {
            var result = await sender.Send(new AddTodoCommand(Todo));

            return Ok(result);
        }

        // Update todo item from this endpoint
        [Authorize]
        [HttpPut("{todoId}")]
        public async Task<ActionResult<TodoEntity>> UpddateTodo([FromRoute]Guid todoId,[FromBody]TodoEntity Todo)
        {
            var result = await sender.Send(new UpdateTodoCommand(todoId,Todo));

            return Ok(result);
        }

        // Delete todo item from this endpoint
        [Authorize]
        [HttpDelete("{todoId}")]
        public async Task<ActionResult<bool>> DeleteTodo(Guid todoId)
        {
            var result = await sender.Send(new DeleteTodoCommand(todoId));

            return Ok(result);
        }

        // Get all todo items from this endpoint 
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<TodoDto>> GetAllTodos()
        {
            var result = await sender.Send(new GetAllTodosQuery());

            return Ok(result);
        }

        //Get all todos by user id from this endpoint for a specific user
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<TodoDto>>> GetTodosByUserId([FromRoute]Guid userId)
        {
            var result = await sender.Send(new GetTodosByUserIdQuery(userId));
            return Ok(result);
        }


        // Get a single todo item by id from this endpoint
        [Authorize]
        [HttpGet("{todoId}")]
        public async Task<ActionResult<TodoDto>> GetTodo([FromRoute]Guid todoId)
        {
            var result = await sender.Send(new GetTodoByIdQuery(todoId));

            return Ok(result);
        }
    }

}
