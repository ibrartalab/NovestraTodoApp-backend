using NovestraTodo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserEntity>> GetUsers();
        Task<UserEntity?> GetUserById(Guid id);
        Task<UserEntity?> GetUserByUsername(string username);
        Task<UserEntity> AddNewUser(UserEntity entity);
        Task<IEnumerable<UserEntity>> UpdateUser(Guid userId, UserEntity entity);
        Task<bool> DeleteUser(Guid userId);
    }
}
