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
    public record GetTodoByIdQuery(int Id):IRequest<TodoDto?>;
    public class GetTodoByIdQueryHandler(ITodoRepository TodoRespository):IRequestHandler<GetTodoByIdQuery,TodoDto?>
    {
        public async Task<TodoDto?>Handle(GetTodoByIdQuery request,CancellationToken cancellationToken)
        {
            var todo = await TodoRespository.GetTodoById(request.Id);

            if(todo == null)
                return null;

            return new TodoDto
            {
                Id = todo.Id,
                Todo = todo.Todo,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                CompletedAt = todo.CompletedAt
            };
        }
    }
}
