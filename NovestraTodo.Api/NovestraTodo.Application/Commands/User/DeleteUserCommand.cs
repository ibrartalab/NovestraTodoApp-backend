using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Commands.User
{
    public record DeleteUserCommand(Guid UserId):IRequest<bool>;
    public class DeleteUserCommandHandler(IUserRepository UserRepository):IRequestHandler<DeleteUserCommand,bool>
    {
        public async Task<bool>Handle(DeleteUserCommand request,CancellationToken cancellationToken)
        {
            return await UserRepository.DeleteUser(request.UserId);
        }
    }
}
