using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;
using System.Security.Claims;

namespace NovestraTodo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
       
        //Register a user from this endpoint
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto User)
        {
            var result = await authService.RegisterUser(User);

            return Ok(result);
        }
        //Login a user and give acess from this endpoint
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto User)
        {
            var result = await authService.LoginUser(User);
            return Ok(result);
        }

    }
}
