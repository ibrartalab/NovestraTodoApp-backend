using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Commands
{
    public record UpdateUserCommand(Guid UserId,UserEntity User):IRequest<UserEntity>;
    public class UpdateUserCommandHandler(IUserRepository UserRepository): IRequestHandler<UpdateUserCommand,UserEntity>
    {
        public async Task<UserEntity>Handle(UpdateUserCommand request,CancellationToken cancellationToken)
        {
            return (UserEntity) await UserRepository.UpdateUser(request.UserId,request.User);
        }
    }
}
