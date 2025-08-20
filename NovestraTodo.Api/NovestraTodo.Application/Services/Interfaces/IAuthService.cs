using NovestraTodo.Application.DTOs;

namespace NovestraTodo.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginUser(LoginRequestDto loginRequest);
    }
}
