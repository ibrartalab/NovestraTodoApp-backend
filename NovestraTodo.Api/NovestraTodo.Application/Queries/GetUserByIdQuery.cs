using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Queries
{
    public record GetUserQuery(Guid userId):IRequest<UserEntity>;
    public class GetUserQueryHandler(IUserRepository userRepository):IRequestHandler<GetUserQuery,UserEntity>
    {
        public async Task<UserEntity>Handle(GetUserQuery request,CancellationToken cancellationToken) {
            return (UserEntity)await userRepository.GetUserById(request.userId);
        }
    }
}
