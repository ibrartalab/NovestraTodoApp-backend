using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.Commands;
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
    }

}
