using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Queries
{
    public record GetAllTodosQuery():IRequest<TodoEntity>;
    public class GetAllTodosQueryHandler(ITodoRepository TodoRepository):IRequestHandler<GetAllTodosQuery,TodoEntity>
    {
        public async Task<TodoEntity>Handle(GetAllTodosQuery request,CancellationToken cancellationToken)
        {
            return (TodoEntity)await TodoRepository.GetTodos();
        }
    }
}
