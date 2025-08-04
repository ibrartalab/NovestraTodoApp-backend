using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.Commands;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Queries;
using NovestraTodo.Core.Entities;
using System.Security.Claims;

namespace NovestraTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] UserEntity User)
        {
            var result = await sender.Send(new RegisterUserCommand(User));

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] UserEntity User)
        {
            var result = await sender.Send(new LoginUserCommand(User));
            return Ok(result);
        }

        //Secure API Endpoints
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMyProfie([FromRoute] Guid userId)
        {
            //var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await sender.Send(new GetUserQuery(userId));
            return Ok(user);
        }
    }
}
