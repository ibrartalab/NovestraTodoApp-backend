using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.Services.Implementation
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserEntity>> GetUsers() => await _userRepository.GetAllUsersAsync();

        public async Task<UserEntity?> GetUserById(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
        public async Task<UserEntity> AddNewUser(UserEntity entity)
        {
            return await _userRepository.AddUserAsync(entity);
        }
        public async Task<IEnumerable<UserEntity>> UpdateUser(Guid userId, UserEntity entity)
        {
            return await _userRepository.UpdateUserAsync(userId, entity);
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}
