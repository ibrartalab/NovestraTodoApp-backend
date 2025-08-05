using MediatR;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands
{
    public record AddTodoCommand(TodoEntity Todo):IRequest<TodoEntity>;
    public class AddTodoCommandHandler:IRequestHandler<AddTodoCommand,TodoEntity>
    {
        private readonly ITodoRepository _todoRepository;

        
        public AddTodoCommandHandler(ITodoRepository TodoRepository)
        {
            _todoRepository = TodoRepository;
        }

        public async Task<TodoEntity> Handle(AddTodoCommand request,CancellationToken cancellationToken) {
            return (TodoEntity) await _todoRepository.AddTodo(request.Todo);
        }

    }
}
