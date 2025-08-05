using NovestraTodo.Core.Entities;


namespace NovestraTodo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<UserEntity?> GetUserById(Guid id);
        Task<UserEntity?> GetUserByUsername(string username);
        Task<UserEntity> AddNewUser(UserEntity entity);
        Task<IEnumerable<UserEntity>> UpdateUser(Guid userId, UserEntity entity);
        Task<bool> DeleteUser(Guid userId);
    }
}
