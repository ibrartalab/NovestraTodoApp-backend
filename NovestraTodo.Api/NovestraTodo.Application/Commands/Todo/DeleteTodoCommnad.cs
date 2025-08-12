using MediatR;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Commands.Todo
{
    public record DeleteTodoCommand(int TodoId):IRequest<bool>;
    public class DeleteTodoCommnadHandler(ITodoRepository TodoRespository):IRequestHandler<DeleteTodoCommand,bool>
    {
       
        public async Task<bool>Handle(DeleteTodoCommand request,CancellationToken cancellationToken)
        {
            return await TodoRespository.DeleteTodo(request.TodoId);
        }
    }
}
