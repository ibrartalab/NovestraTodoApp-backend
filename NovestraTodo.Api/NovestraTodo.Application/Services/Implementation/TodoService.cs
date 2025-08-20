using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;

namespace NovestraTodo.Application.Services.Implementation
{
    public class TodoService:ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoEntity>> GetTodos() => await _todoRepository.GetAllAsync();

        public async Task<List<TodoEntity>> GetUserTodos(Guid userId) => await _todoRepository.GetByUserIdAsync(userId);
        public async Task<TodoEntity> AddTodo(TodoEntity todoEntity)
        {
            return await _todoRepository.AddAsync(todoEntity);
        }

        public async Task<TodoEntity>UpdateTodo(int id,TodoEntity todoEntity)
        {
            return await _todoRepository.UpdateAsync(id,todoEntity);
        }

        public async Task<bool> DeleteTodo(int id)
        {
            return await _todoRepository.DeleteAsync(id);
        }
    }
}
