using Microsoft.EntityFrameworkCore;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using NovestraTodo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Infrastructure.Repositories
{
    public class TodoRepository(NovestraDbContext dbContext):ITodoRepository
    {
        // Get all the todos list from the db
        public async Task<IEnumerable<TodoEntity>> GetTodos()
        {
            return await dbContext.Todos.ToListAsync();
        }

        //Get all todos by user id for a specific user
        public async Task<List<TodoEntity>>GetTodosByUserId(Guid userId)
        {
            return await dbContext.Todos
                .Where(todo => todo.UserId == userId)
                .ToListAsync();
        }
        // Get todo by id
        public async Task<TodoEntity?> GetTodoById(Guid id)
        {
            return  await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == id) as TodoEntity;

        }

        // Add new todo
        public async Task<TodoEntity> AddTodo(TodoEntity entity)
        {
            entity.Id = Guid.NewGuid();
            dbContext.Todos.Add(entity);

            await dbContext.SaveChangesAsync();

            return (TodoEntity)entity;
        }

        // Update a user
        public async Task<TodoEntity> UpdateTodo(Guid todoId, TodoEntity entity)
        {
            var todo = await dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id == todoId);

            if (todo is not null)
            {
                todo.Todo = entity.Todo;
                todo.IsCompleted = entity.IsCompleted;
                todo.CompletedAt = entity.CompletedAt;

                await dbContext.SaveChangesAsync();

                return (TodoEntity)todo;
            }

            return (TodoEntity)entity;
        }

        // Delete a user
        public async Task<bool> DeleteTodo(Guid todoId)
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
