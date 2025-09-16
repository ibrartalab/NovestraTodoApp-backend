using NovestraTodo.Application.DTOs;
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
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto?> GetUserById(Guid id);
        Task<UserDto?> GetUserByUsername(string username);
        Task<UserDto> AddNewUser(UserEntity entity);
        Task<UserDto> UpdateUser(Guid userId, UserEntity entity);
        Task<bool> DeleteUser(Guid userId);
    }
}
