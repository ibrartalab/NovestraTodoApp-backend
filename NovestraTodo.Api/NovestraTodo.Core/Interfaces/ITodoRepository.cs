using NovestraTodo.Core.Entities;

namespace NovestraTodo.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoEntity>> GetTodos();
        Task<List<TodoEntity>> GetTodosByUserId(Guid userId);
        Task<TodoEntity?> GetTodoById(int id);
        Task<TodoEntity> AddTodo(TodoEntity entity);
        Task<TodoEntity> UpdateTodo(int todoId, TodoEntity entity);
        Task<bool> DeleteTodo(int todoId);
    }
}
