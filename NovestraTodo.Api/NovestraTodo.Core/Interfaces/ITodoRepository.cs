
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoEntity>> GetTodos();
        Task<List<TodoEntity>> GetTodosByUserId(Guid userId);
        Task<TodoEntity?> GetTodoById(Guid id);
        Task<TodoEntity> AddTodo(TodoEntity entity);
        Task<TodoEntity> UpdateTodo(Guid todoId, TodoEntity entity);
        Task<bool> DeleteTodo(Guid todoId);
    }
}
