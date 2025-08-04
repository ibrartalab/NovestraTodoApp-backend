using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Queries
{
    public record GetUserQuery(Guid userId):IRequest<UserDto>;
    public class GetUserQueryHandler(IUserRepository userRepository):IRequestHandler<GetUserQuery,UserDto>
    {
        public async Task<UserDto>Handle(GetUserQuery request,CancellationToken cancellationToken) {

            var user = await userRepository.GetUserById(request.userId);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };
        }
    }
}
