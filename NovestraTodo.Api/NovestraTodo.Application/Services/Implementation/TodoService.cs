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

        public async Task<IEnumerable<TodoEntity?>> GetTodos() => await _todoRepository.GetAllAsync();
    }
}
