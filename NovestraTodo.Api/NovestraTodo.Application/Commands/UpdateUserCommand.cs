using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Commands
{
    public record UpdateUserCommand(Guid userId,UserEntity user):IRequest<UserEntity>;
    public class UpdateUserCommandHandler(IUserRepository userRepository): IRequestHandler<UpdateUserCommand,UserEntity>
    {
        public async Task<UserEntity>Handle(UpdateUserCommand request,CancellationToken cancellationToken)
        {
            return (UserEntity) await userRepository.UpdateUser(request.userId,request.user);
        }
    }
}
