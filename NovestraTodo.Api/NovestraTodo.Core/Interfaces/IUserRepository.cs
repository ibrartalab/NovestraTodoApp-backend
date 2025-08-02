using NovestraTodo.Core.Entities;


namespace NovestraTodo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers();
    }
}
