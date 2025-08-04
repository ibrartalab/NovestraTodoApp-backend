using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands
{
    public record LoginUserCommand(UserEntity User):IRequest<AuthResponseDto>;
    public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand,AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtService jwtService) { 
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto>Handle(LoginUserCommand request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.User.Id);

            //if (user == null || !BCrypt.Net.BCrypt.Verify(request.User.Password, user.Password){
            //    throw new UnauthorizedAccessException("Invalid Credentials");
            //}
                

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
                    CreatedAt = DateTime.UtcNow,
                }
                };
        }
    }
}
