    using MediatR;
using NovestraTodo.Application.DTOs;
using NovestraTodo.Core.Entities;
    using NovestraTodo.Core.Interfaces;


    namespace NovestraTodo.Application.Queries
    {
        public record GetAllUsersQuery():IRequest<IEnumerable<UserDto>>;
        public class GetAllUsersQueryHandler (IUserRepository userRepository):IRequestHandler<GetAllUsersQuery,IEnumerable<UserDto>>
        {
            public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request,CancellationToken cancellationToken) {
                var users = await userRepository.GetUsers();

            return users.Select(u => new UserDto { 
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            });

            
            }
        }
    }
