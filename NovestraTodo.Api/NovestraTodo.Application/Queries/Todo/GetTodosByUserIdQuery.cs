using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Queries.Todo
{
    public record GetTodosByUserIdQuery(Guid UserId) : IRequest<List<TodoEntity>>;
    public class GetTodosByUserIdQueryHandler(ITodoRepository TodoRepository) : IRequestHandler<GetTodosByUserIdQuery, List<TodoEntity>>
    {
        public async Task<List<TodoEntity>> Handle(GetTodosByUserIdQuery request, CancellationToken cancellationToken)
        {
            var todos = await TodoRepository.GetTodosByUserId(request.UserId);
            return  todos;
        }
    }
}
