using NovestraTodo.Core.Entities;


namespace NovestraTodo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetUserByIdAsync(Guid id);
        Task<UserEntity?> GetUserByUsernameAsync(string username);
        Task<UserEntity> AddUserAsync(UserEntity entity);
        Task<IEnumerable<UserEntity>> UpdateUserAsync(Guid userId, UserEntity entity);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
