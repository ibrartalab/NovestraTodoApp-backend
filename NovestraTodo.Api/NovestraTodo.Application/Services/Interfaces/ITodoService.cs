using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoEntity>> GetTodos();
        Task<List<TodoEntity>> GetUserTodos(Guid userId);
        Task<TodoEntity> AddTodo(TodoEntity todo);
        Task<TodoEntity> UpdateTodo(int id, TodoEntity todo);
        Task<bool> DeleteTodo(int id);
    }
}
