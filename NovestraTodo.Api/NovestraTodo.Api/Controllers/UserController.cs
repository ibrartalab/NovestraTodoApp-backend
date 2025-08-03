using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.Commands;
using NovestraTodo.Application.Queries;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddUser([FromBody] UserEntity user)
        {
            var result = await sender.Send(new AddUserCommand(user));
            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            return Ok();
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid userId)
        {
            var result = await sender.Send(new GetUserQuery(userId));
            return Ok(result);
        }

        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserEntity user)
        {
            var result = await sender.Send(new UpdateUserCommand(userId,user));
            return Ok(result);
        }
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var result = await sender.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }

}
