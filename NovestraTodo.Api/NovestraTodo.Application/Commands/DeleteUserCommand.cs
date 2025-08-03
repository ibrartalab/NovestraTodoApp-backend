using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Commands
{
    public record DeleteUserCommand(Guid userId):IRequest<bool>;
    public class DeleteUserCommandHandler(IUserRepository userRepository):IRequestHandler<DeleteUserCommand,bool>
    {
        public async Task<bool>Handle(DeleteUserCommand request,CancellationToken cancellationToken)
        {
            return (bool)await userRepository.DeleteUser(request.userId);
        }
    }
}
