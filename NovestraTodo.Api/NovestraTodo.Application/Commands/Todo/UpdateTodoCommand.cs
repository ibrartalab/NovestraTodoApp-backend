using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands.Todo
{
    public record UpdateTodoCommand(Guid TodoId,TodoEntity Todo):IRequest<TodoDto>;
    public class UpdateTodoCommandHandler(ITodoRepository TodoRepository): IRequestHandler<UpdateTodoCommand,TodoDto>
    {
        //private readonly ITodoRepository _todoRepository;

        //public UpdateTodoCommandHandler(this ITodoRepository TodoRepository) {
        //    _todoRepository = (ITodoRepository?) TodoRepository;
        //}

        public async Task<TodoDto>Handle(UpdateTodoCommand request,CancellationToken cancellationToken)
        {
            return (TodoDto) await TodoRepository.UpdateTodo(request.TodoId, request.Todo);
        }
    }
}
