using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands.Todo
{
    public record AddTodoCommand(TodoEntity Todo):IRequest<TodoDto>;
    public class AddTodoCommandHandler:IRequestHandler<AddTodoCommand, TodoDto>
    {
        private readonly ITodoRepository _todoRepository;

        
        public AddTodoCommandHandler(ITodoRepository TodoRepository)
        {
            _todoRepository = TodoRepository;
        }

        public async Task<TodoDto> Handle(AddTodoCommand request,CancellationToken cancellationToken) {
            var todo = await _todoRepository.GetTodoById(request.Todo.Id);

            if (todo is not null)
            {
                throw new Exception("Todo already exists!");
            }

            var newTodo = new TodoEntity
            {
                
                Todo = request.Todo.Todo,
                IsCompleted = request.Todo.IsCompleted,
                CreatedAt = DateTime.UtcNow,
                CompletedAt = request.Todo.CompletedAt,
                UserId = request.Todo.UserId
            };

            await _todoRepository.AddTodo(newTodo);

            return new TodoDto
            {
                Id=newTodo.Id,
                Todo = newTodo.Todo,
                IsCompleted = newTodo.IsCompleted,
                CreatedAt = newTodo.CreatedAt,
                CompletedAt = newTodo.CompletedAt,
                UserId = newTodo.UserId
            };

        }

    }
}
