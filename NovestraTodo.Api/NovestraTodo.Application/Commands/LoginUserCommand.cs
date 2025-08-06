using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands
{
    public record LoginUserCommand(LoginRequestDto User):IRequest<AuthResponseDto?>;
    public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand,AuthResponseDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IUserRepository UserRepository, IJwtService JwtService) { 
            _jwtService = JwtService;
            _userRepository = UserRepository;
        }

        public async Task<AuthResponseDto?>Handle(LoginUserCommand request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(request.User.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.User.Password, user.Password)){
                throw new UnauthorizedAccessException("Invalid Credentials");
            }


            var token = _jwtService.GenerateToken(user);
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
