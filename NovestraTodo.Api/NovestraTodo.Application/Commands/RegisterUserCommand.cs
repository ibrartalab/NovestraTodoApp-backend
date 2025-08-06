using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands
{
    public record RegisterUserCommand(RegisterRequestDto User):IRequest<AuthResponseDto>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public RegisterUserCommandHandler(IUserRepository UserRepository,IJwtService JwtService)
        {
            _userRepository = UserRepository;
            _jwtService = JwtService;
        }

        public async Task<AuthResponseDto>Handle(RegisterUserCommand request,CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByUsername(request.User.Username);

            if (existingUser != null)
                throw new Exception("User already exist!");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.User.Password);

            var newUser = new UserEntity
            {
                
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                UserName = request.User.Username,
                Email = request.User.Email,
                Password = passwordHash,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.AddNewUser(newUser);

            var token = _jwtService.GenerateToken(newUser);
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
    }
    
}
