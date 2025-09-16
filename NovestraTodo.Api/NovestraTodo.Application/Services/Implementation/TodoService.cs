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

        public async Task<IEnumerable<TodoDto>> GetTodos()
        {
            var todos = await _todoRepository.GetAllAsync();
            var todoDtos = todos.Select(todo => new TodoDto
            {
                Id = todo.Id,
                Todo = todo.Todo,
                IsCompleted = todo.IsCompleted,
                CompletedAt = todo.CompletedAt,
                IsRemoved = todo.IsRemoved,
                UserId = todo.UserId
            });
            return todoDtos;
        }

        public async Task<IEnumerable<TodoDto>> GetUserTodos(Guid userId){
                var todos = await _todoRepository.GetByUserIdAsync(userId);
                var todoDtos = todos.Select(todo => new TodoDto
                {
                    Id = todo.Id,
                    Todo = todo.Todo,
                    IsCompleted = todo.IsCompleted,
                    CompletedAt = todo.CompletedAt,
                    IsRemoved = todo.IsRemoved,
                    UserId = todo.UserId
                });
                return todoDtos;
        }

        public async Task<TodoDto> AddTodo(TodoEntity todoEntity)
        {
            var newTodo = await _todoRepository.AddAsync(todoEntity);

            var todoDto = new TodoDto
            {
                Id = newTodo.Id,
                Todo = newTodo.Todo,
                IsCompleted = newTodo.IsCompleted,
                CompletedAt = newTodo.CompletedAt,
                IsRemoved = newTodo.IsRemoved,
                UserId = newTodo.UserId
            };
            return todoDto;
        }

        public async Task<TodoDto>UpdateTodo(int id,TodoEntity todoEntity)
        {
            var updatedTodo =  await _todoRepository.UpdateAsync(id,todoEntity);

            var todoDto = new TodoDto
            {
                Id = updatedTodo.Id,
                Todo = updatedTodo.Todo,
                IsCompleted = updatedTodo.IsCompleted,
                CompletedAt = updatedTodo.CompletedAt,
                IsRemoved = updatedTodo.IsRemoved,
                UserId = updatedTodo.UserId
            };
            return todoDto;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            return await _todoRepository.DeleteAsync(id);
        }
    }
}
