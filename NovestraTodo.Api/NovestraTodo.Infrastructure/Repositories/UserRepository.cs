using Microsoft.EntityFrameworkCore;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using NovestraTodo.Infrastructure.Data;

namespace NovestraTodo.Infrastructure.Repositories
{
    public class UserRepository(NovestraDbContext dbContext) : IUserRepository
    {
        // Get all the users list from the db
        public async Task<IEnumerable<UserEntity>> GetUsers()
        {
            return await dbContext.Users.ToListAsync();
        }
        // Get user by id
        public async Task<UserEntity?> GetUserById(Guid id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            
        }

        // Get user by username
        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
        }

        // Add a new user
        public async Task<UserEntity>AddNewUser(UserEntity entity)
        {
            entity.Id = Guid.NewGuid();
            dbContext.Users.Add(entity);

            await dbContext.SaveChangesAsync();

            return (UserEntity)entity;
        }

        // Update a user
        public async Task<IEnumerable<UserEntity>>UpdateUser(Guid userId,UserEntity entity)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if(user is not null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.Password = entity.Password;

                await dbContext.SaveChangesAsync();

                return (IEnumerable<UserEntity>) user;
            }

            return (IEnumerable<UserEntity>)entity;
        }

        // Delete a user
        public async Task<bool>DeleteUser(Guid userId)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if(user is not null)
            {
                dbContext.Remove(user);

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;

        }
    }
}
