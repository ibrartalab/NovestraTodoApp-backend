using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Queries.Todo
{
    public record GetAllTodosQuery():IRequest<IEnumerable<TodoDto>>;
    public class GetAllTodosQueryHandler(ITodoRepository TodoRepository):IRequestHandler<GetAllTodosQuery,IEnumerable<TodoDto>>
    {
        public async Task<IEnumerable<TodoDto>> Handle(GetAllTodosQuery request,CancellationToken cancellationToken)
        {
            var todos = await TodoRepository.GetTodos();

            return todos.Select(todo => new TodoDto
            {
                Id = todo.Id,
                Todo = todo.Todo,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                CompletedAt = todo.CompletedAt
            });
        }
    }
}
