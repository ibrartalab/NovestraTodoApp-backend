using Microsoft.EntityFrameworkCore;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using NovestraTodo.Infrastructure.Data;


namespace NovestraTodo.Infrastructure.Repositories
{
    public class TodoRepository(NovestraDbContext dbContext):ITodoRepository
    {
        // Get all the todos list from the db
        public async Task<IEnumerable<TodoEntity>> GetAllAsync()
        {
            return await dbContext.Todos.ToListAsync();
        }

        //Get all todos by user id for a specific user
        public async Task<List<TodoEntity>>GetByUserIdAsync(Guid userId)
        {
            return await dbContext.Todos
                .Where(todo => todo.UserId == userId)
                .ToListAsync();
        }
        // Get todo by id
        public async Task<TodoEntity?> GetByIdAsync(int id)
        {
            return  await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id) as TodoEntity;

        }

        // Add new todo
        public async Task<TodoEntity> AddAsync(TodoEntity entity)
        {
            
            dbContext.Todos.Add(entity);

            await dbContext.SaveChangesAsync();

            return (TodoEntity)entity;
        }

        // Update a user
        public async Task<TodoEntity> UpdateAsync(int todoId, TodoEntity entity)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == todoId);

            if (todo is not null)
            {
                todo.Todo = entity.Todo;
                todo.IsCompleted = entity.IsCompleted;
                todo.CompletedAt = entity.CompletedAt;
                todo.IsRemoved = entity.IsRemoved;

                await dbContext.SaveChangesAsync();

                return (TodoEntity)todo;
            }

            return (TodoEntity)entity;
        }

        // Delete a user
        public async Task<bool> DeleteAsync(int todoId)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == todoId);

            if (todo is not null)
            {
                dbContext.Remove(todo);

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
