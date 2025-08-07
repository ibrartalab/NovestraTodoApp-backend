
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoEntity>> GetTodos();
        Task<TodoEntity?> GetTodoById(Guid id);
        Task<TodoEntity> AddTodo(TodoEntity entity);
        Task<IEnumerable<TodoEntity>> UpdateTodo(Guid todoId, TodoEntity entity);
        Task<bool> DeleteTodo(Guid todoId);
    }
}
