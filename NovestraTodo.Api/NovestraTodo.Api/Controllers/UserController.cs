using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.Commands;
using NovestraTodo.Application.Commands.User;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Queries;
using NovestraTodo.Application.Queries.User;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        // get all users from this endpoint
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            
            return Ok(result);
        }

        // get a single user by id form this endpoint
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] Guid userId)
        {
            var result = await sender.Send(new GetUserQuery(userId));
 
            return Ok(result);
        }

        // update an existing user by prividing id and updatedInfo from this endpoint
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserEntity user)
        {
            var result = await sender.Send(new UpdateUserCommand(userId,user));
            return Ok(result);
        }

        // delete a user from the database permanently from this endpoint
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var result = await sender.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }

}
