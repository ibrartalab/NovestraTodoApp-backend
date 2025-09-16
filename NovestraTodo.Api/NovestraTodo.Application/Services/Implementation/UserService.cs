using NovestraTodo.Application.DTOs;
using NovestraTodo.Application.Services.Interfaces;
using NovestraTodo.Core.Entities;
using NovestraTodo.Core.Interfaces;


namespace NovestraTodo.Application.Services.Implementation
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
            });
            return userDtos;
        }

        public async Task<UserDto?> GetUserById(Guid id)
        {
             var user = await _userRepository.GetUserByIdAsync(id);
             var userDto = user == null ? null : new UserDto
             {
                 Id = user.Id,
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 UserName = user.UserName,
                 Email = user.Email,
             };
                return userDto;
        }
        public async Task<UserDto?> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            var userDto = user == null ? null : new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
            };
            return userDto;
        }
        public async Task<UserDto> AddNewUser(UserEntity entity)
        {
            var user = await _userRepository.GetUserByUsernameAsync(entity.UserName);
            var newUser =  await _userRepository.AddUserAsync(entity);

            if(user != null)
            {
                throw new Exception("Username already exists");
            }

            var userDto =  new UserDto
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.UserName,
                Email = newUser.Email,
            };
            return userDto;
        }
        public async Task<UserDto> UpdateUser(Guid userId, UserEntity entity)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var updatedUser = await _userRepository.UpdateUserAsync(userId, entity);
            
            var userDto = new UserDto
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                UserName = updatedUser.UserName,
                Email = updatedUser.Email,
            };
            return userDto;
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}
