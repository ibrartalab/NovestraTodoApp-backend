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
        Task<IEnumerable<TodoEntity?>> GetTodos();
        //Task AddTodo(TodoEntity todo);
        //Task UpdateTodo(int  id, TodoEntity todo);
        //Task DeleteTodo(int id);
    }
}
