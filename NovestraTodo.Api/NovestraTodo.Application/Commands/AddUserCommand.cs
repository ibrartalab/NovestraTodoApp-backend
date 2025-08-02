using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Commands
{
    public record AddUserCommand(UserEntity User):IRequest<UserEntity>;

    public class AddUserCommandHandler(IUserRepository userRepository) : IRequestHandler<AddUserCommand, UserEntity>
    {
        public async Task<UserEntity> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return (UserEntity) await userRepository.AddNewUser(request.User);
        }
    }

}
