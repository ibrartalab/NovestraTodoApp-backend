using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Services.Implementation
{
    public class AuthService(IUserRepository userRepository, IJwtService jwtService) : IAuthService
    {
        
        public async Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequest)
        {
            var existingUser = await userRepository.GetUserByUsernameAsync(registerRequest.Username);

            if (existingUser != null)
                throw new Exception("User already exist!");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            var newUser = new UserEntity
            {

                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                Password = passwordHash,
                CreatedAt = DateTime.UtcNow,
            };

            await userRepository.AddUserAsync(newUser);

            var token = jwtService.GenerateToken(newUser);
            return new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    CreatedAt = newUser.CreatedAt

                }
            };
        }

        public async Task<AuthResponseDto> LoginUser(LoginRequestDto loginRequest)
        {
            var user = await userRepository.GetUserByUsernameAsync(loginRequest.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid Credentials");
            }


            var token = jwtService.GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    CreatedAt = DateTime.UtcNow,
                }
            };
        }
    }
}
