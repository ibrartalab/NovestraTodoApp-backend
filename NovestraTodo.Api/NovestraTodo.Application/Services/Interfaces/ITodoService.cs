using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;


namespace NovestraTodo.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetTodos();
        Task<IEnumerable<TodoDto>> GetUserTodos(Guid userId);
        Task<TodoDto> AddTodo(TodoEntity todo);
        Task<TodoDto> UpdateTodo(int id, TodoEntity todo);
        Task<bool> DeleteTodo(int id);
    }
}
