using NovestraTodo.Core.Entities;


namespace NovestraTodo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<IEnumerable<UserEntity>> GetUserById(Guid id);
        Task<IEnumerable<UserEntity>> AddNewUser(UserEntity entity);
        Task<IEnumerable<UserEntity>> UpdateUser(Guid userId, UserEntity entity);
        Task<bool> DeleteUser(Guid userId);
    }
}
