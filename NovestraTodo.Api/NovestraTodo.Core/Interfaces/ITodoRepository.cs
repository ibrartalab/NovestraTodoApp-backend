using NovestraTodo.Core.Entities;

namespace NovestraTodo.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoEntity>> GetAllAsync();
        Task<List<TodoEntity>> GetByUserIdAsync(Guid userId);
        Task<TodoEntity?> GetByIdAsync(int id);
        Task<TodoEntity> AddAsync(TodoEntity entity);
        Task<TodoEntity> UpdateAsync(int todoId, TodoEntity entity);
        Task<bool> DeleteAsync(int todoId);
    }
}
