using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands.Todo
{
    public record UpdateTodoCommand(int TodoId,TodoEntity Todo):IRequest<TodoEntity>;
    public class UpdateTodoCommandHandler(ITodoRepository TodoRepository): IRequestHandler<UpdateTodoCommand,TodoEntity>
    {
        //private readonly ITodoRepository _todoRepository;

        //public UpdateTodoCommandHandler(this ITodoRepository TodoRepository) {
        //    _todoRepository = (ITodoRepository?) TodoRepository;
        //}

        public async Task<TodoEntity>Handle(UpdateTodoCommand request,CancellationToken cancellationToken)
        {
            return (TodoEntity) await TodoRepository.UpdateTodo(request.TodoId, request.Todo);
        }
    }
}
