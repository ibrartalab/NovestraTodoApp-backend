using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Queries
{
    public record GetTodoByIdQuery(Guid TodoId):IRequest<TodoEntity?>;
    public class GetTodoByIdQueryHandler(ITodoRepository TodoRespository):IRequestHandler<GetTodoByIdQuery,TodoEntity?>
    {
        public async Task<TodoEntity?>Handle(GetTodoByIdQuery request,CancellationToken cancellationToken)
        {
            return await TodoRespository.GetTodoById(request.TodoId);
        }
    }
}
