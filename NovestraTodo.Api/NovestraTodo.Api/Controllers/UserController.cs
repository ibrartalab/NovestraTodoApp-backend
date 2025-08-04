using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.Commands;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Queries;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        //[HttpPost("")]
        ////public async Task<IActionResult> AddUser([FromBody] UserEntity user)
        ////{
        ////    var result = await sender.Send(new AddUserCommand(user));
        ////    return Ok(result);
        ////}

        //[HttpGet("all")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var result = await sender.Send(new GetAllUsersQuery());
        //    return Ok(result);
        //}
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var result = await sender.Send(new GetAllUsersQuery());
            
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser([FromRoute] Guid userId)
        {
            var result = await sender.Send(new GetUserQuery(userId));
 
            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserEntity user)
        {
            var result = await sender.Send(new UpdateUserCommand(userId,user));
            return Ok(result);
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var result = await sender.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }

}
